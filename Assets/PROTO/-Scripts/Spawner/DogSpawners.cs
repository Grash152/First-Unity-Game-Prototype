using PROTO._Scripts.Characters.Dog;
using UnityEngine;
using UnityEngine.Pool;

namespace PROTO._Scripts.Spawner
{
    public class DogSpawners : MonoBehaviour
    { 
        [SerializeField] private DogBounds dog;
        [SerializeField] private float spawnCooldown = 5f;
        [SerializeField] [Range(0f,359f)] private float startAngle;
        [SerializeField] private LaunchPattern pattern = LaunchPattern.BackAndForth;
        private float _spawnTime;
        private Vector2 _catPos;
        private ObjectPool<GameObject> _pool;
        private Transform _playerTransform;
        private float _angle;
        private bool _forth;
        private bool _shoot;
        public static DogSpawners Instance { get; private set; }
        private enum LaunchPattern
        {
            None,
            BackAndForth,
            Aimed,
            Random
        }
        private void Awake()
        {
            Instance = this;
            _pool = new ObjectPool<GameObject>(
                createFunc: CreateItem,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroyItem,
                collectionCheck: true,   // helps catch double-release mistakes
                defaultCapacity: 10,
                maxSize: 50
            );
        }

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _playerTransform = player.transform;
            }
            _angle = startAngle;
        }
        private void Update()
        {
            _spawnTime+=Time.deltaTime;
            _catPos = _playerTransform.position;
            Vector2 direction = (_catPos - (Vector2)transform.position ).normalized;   
            if (_spawnTime >= spawnCooldown)
            {
                switch (pattern)
                {
                    case LaunchPattern.None:
                        _shoot = false;
                        break;
                    case LaunchPattern.BackAndForth:
                        _shoot = true;
                        if (_forth)
                        {
                            _angle -= 10f;
                        }
                        else
                        {
                            _angle += 10f;
                        }
                        float angleDifference = _angle - startAngle;
                        if (angleDifference >= 90f)
                        {
                            _forth = true;
                        }
                        else if (angleDifference <= -90f)
                        {
                            _forth = false;
                        }
                        break;
                    case LaunchPattern.Aimed:
                        _shoot = true;
                        _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                        break;
                    case LaunchPattern.Random:
                        _shoot = true;
                        _angle = Random.Range(-45f, 45f) + startAngle;
                        break;
                }
        

// Get the SpriteRenderer component from the spawned dog
                if(_shoot) 
                    SpawnDog();
                _spawnTime = 0;
            
            }
        
        }

        private void SpawnDog()
        {
        
            GameObject husky = _pool.Get();
            husky.transform.position = transform.position;
            husky.transform.rotation = Quaternion.Euler(0,0,_angle);
            StartCoroutine(ReturnAfter(husky, 2f));
        }
        // Creates a new pooled GameObject the first time (and whenever the pool needs more).
        private GameObject CreateItem()
        {
            GameObject pooledObject = Instantiate(dog.gameObject);
            
            pooledObject.name = "Dog";
            pooledObject.SetActive(false);
            return pooledObject;
        }
 
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
            // Give it back to the pool.
            _pool.Release(pooledObject);
        }
    }
}
