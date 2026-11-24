using UnityEngine;
using UnityEngine.Pool;

namespace PROTO._Scripts.RockThrow
{
    public class RockThrow : MonoBehaviour
    {
        [SerializeField] private Projectile myProjectile;
        public float projectileLifeTime = 2f;
        public float scaleMultiplier = 10f;
        private Vector2 _mousePos;
        private ObjectPool<GameObject> _pool;
        private Camera _mainCamera;
        public float fireRate = 0.5f;
        private float _nextFireTime;
        public static bool Bigger;
        private float _biggerTimer;
        private const float BiggerCooldown = 3f;
        private Vector3 _originalScale;
        private bool _hasSavedScale;
        private void Awake()
        {
            
            _pool = new ObjectPool<GameObject>(
                createFunc: CreateItem,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroyItem,
                collectionCheck: true,   // helps catch double-release mistakes
                defaultCapacity: 10,
                maxSize: 50
            );
            PrewarmPool();
        }
        private void PrewarmPool()
        {
            for (int i = 0; i < 10; i++)
                CreateItem();

       
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }
        private void Update()
        {
            if (Bigger)
            {
                _biggerTimer += Time.deltaTime;
                if (_biggerTimer >= BiggerCooldown)
                {
                    _biggerTimer = 0;
                    Bigger = false;
                }
            }

            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && Time.time >= _nextFireTime)
            {  
                _nextFireTime = Time.time + fireRate;
                _mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (_mousePos - (Vector2)transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                GameObject proj = _pool.Get();
                proj.transform.position = transform.position;
                proj.transform.rotation = rotation;
                if (proj.TryGetComponent<Projectile>(out var projectileScript))
                {
                    if (Bigger)
                    {
                        proj.transform.localScale = _originalScale * scaleMultiplier;
                        projectileScript.ProjectileBigger(scaleMultiplier); 
                    }
                    else
                    {
                        proj.transform.localScale = _originalScale;
                        projectileScript.ProjectileBigger(1f); 
                    }
                }

        

                StartCoroutine(ReturnAfter(proj, projectileLifeTime));

            }
        }
        // Creates a new pooled GameObject the first time (and whenever the pool needs more).
        private GameObject CreateItem()
        {
            GameObject pooledObject = Instantiate(myProjectile.gameObject);
            pooledObject.name = "Rock";
            if (!_hasSavedScale)
            {
                _originalScale = pooledObject.transform.localScale;
                _hasSavedScale = true;
            }
            pooledObject.SetActive(false);
            return pooledObject;
        }

        // Called when an item is taken from the pool.
        private static void OnGet(GameObject pooledObject)
        {
            pooledObject.SetActive(true);
        }

        // Called when an item is returned to the pool.
        public static void OnRelease(GameObject pooledObject)
        {
            pooledObject.SetActive(false);
        }

        // Called when the pool decides to destroy an item (e.g., above max size).
        private static void OnDestroyItem(GameObject pooledObject)
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