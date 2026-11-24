using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Tilemaps;

namespace PROTO._Scripts.Spawner
{
    public class DestructiblesSpawner : MonoBehaviour
    {
        [SerializeField] private Destructibles.Destructibles healthBush;
        [SerializeField] private Destructibles.Destructibles bush;
        [SerializeField] private Destructibles.Destructibles mushroom;
        private ObjectPool<GameObject> _pool;
        private const float SpawnTimer = 4.0f;
        private float _nextSpawnTime;
        public static DestructiblesSpawner Instance { get; private set; }
        private int _rand;
        public Tilemap obstacleTilemap;
        private const float LifeTime = 40.0f;
        [SerializeField] private int minBushes = 5;
        [SerializeField] private int minHealthBushes = 3;
        [SerializeField] private int minMushrooms = 2;
     
        private void Awake()
        {
            Instance = this;
            _pool = new ObjectPool<GameObject>(
                createFunc: CreateRandomItem,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroyItem,
                collectionCheck: true,   // helps catch double-release mistakes
                defaultCapacity: 10,
                maxSize: 30
            );
            PrewarmPool();
        }

        private void PrewarmPool()
        {
            for (int i = 0; i < minBushes; i++)
                _pool.Release(CreateItem(bush.gameObject));

            for (int i = 0; i < minHealthBushes; i++)
                _pool.Release(CreateItem(healthBush.gameObject));

            for (int i = 0; i < minMushrooms; i++)
                _pool.Release(CreateItem(mushroom.gameObject));
        }
        private void Update()
        {
      
            if( Time.time >= _nextSpawnTime)
            {
                _nextSpawnTime = Time.time + SpawnTimer;
                _rand = Random.Range(0, 3);
                GameObject pooledObject = _pool.Get();
                pooledObject.transform.position = RandomPosition();
                Vector3Int targetCell = obstacleTilemap.WorldToCell(pooledObject.transform.position);
                while (obstacleTilemap.HasTile(targetCell))
                {
                    pooledObject.transform.position = RandomPosition();
                    targetCell = obstacleTilemap.WorldToCell(pooledObject.transform.position);
                }
                StartCoroutine(ReturnAfter(pooledObject, LifeTime));
            }
        }

        private Vector3 RandomPosition()
        {
            float randomX = Random.Range(-8f, 8f);
            float randomY = Random.Range(-6f, 6f);
            
            return new Vector3(randomX, randomY, 0f);
        }
        // Creates a new pooled GameObject the first time (and whenever the pool needs more).
        private GameObject CreateItem(GameObject prefab)
        {

            GameObject pooledObject = Instantiate(prefab);
            pooledObject.name = "Destructibles";
            pooledObject.SetActive(false);
            return pooledObject;
        }

        private GameObject CreateRandomItem()
        {
            switch (_rand)
            {
                case 0:
                    return CreateItem(bush.gameObject);
                case 1:
                    return CreateItem(healthBush.gameObject);
                case 2:
                    return CreateItem(mushroom.gameObject);
                default:
                    return CreateItem(bush.gameObject);
            }
        }

        // Called when an item is taken from the pool.
        private void OnGet(GameObject pooledObject)
        {
            pooledObject.SetActive(true);
        }

        // Called when an item is returned to the pool.
        public void OnRelease(GameObject pooledObject)
        {
            pooledObject.SetActive(false);
        }

        // Called when the pool decides to destroy an item (e.g., above max size).
        private void OnDestroyItem(GameObject pooledObject)
        {
            Destroy(pooledObject);
        }

        private System.Collections.IEnumerator ReturnAfter(GameObject pooledObject, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            if (pooledObject.TryGetComponent<Destructibles.Destructibles>(out var destructibleComponent))
            {
                destructibleComponent.noDrops = true;
            }
            _pool.Release(pooledObject);
            
        }
    }
}

