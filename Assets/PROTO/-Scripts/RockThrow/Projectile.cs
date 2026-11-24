using PROTO._Scripts.Characters.Dog;
using PROTO._Scripts.Collectibles;
using PROTO._Scripts.Spawner;
using UnityEngine;

namespace PROTO._Scripts.RockThrow
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] public Sprite newSprite;
        [SerializeField] public Vector3 boundsSize = new Vector3(0.15f, 0.15f,1f);
        public Vector3 boundsCenter = Vector3.zero;
        private Vector3 _originalBoundsSize;
        private bool _hasSavedBounds;
        
        private SpriteRenderer _spriteRenderer;
        private Sprite _originalSprite;


        public Bounds myBounds;
        
        
        private void Start()
        {
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalSprite = _spriteRenderer.sprite;
        }

        private void Awake()
        {
            myBounds = new Bounds(boundsCenter, boundsSize);
            boundsCenter = transform.position;
            if (!_hasSavedBounds)
            {
                _originalBoundsSize = boundsSize;
                _hasSavedBounds = true;
            }
        }

        public void ProjectileBigger(float multiplier)
        {
            if (!_hasSavedBounds)
            {
                _originalBoundsSize = boundsSize;
                _hasSavedBounds = true;
            }
            boundsSize = _originalBoundsSize * multiplier;
        }
    
        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(myBounds.center, myBounds.size);
        }
        private void Update()
        {
            
            
            boundsCenter = transform.position; //mise à jour
            myBounds = new Bounds(boundsCenter, boundsSize);
            if (transform.position.x < -9000f) return;
            DogBounds[] allEnemies = Object.FindObjectsByType<DogBounds>();
            foreach (DogBounds enemy in allEnemies)
            {
                if (!gameObject.activeInHierarchy) break;
                if (myBounds.Intersects(enemy.dogBounds))
                {
                        DogSpawners.Instance.OnRelease(enemy.gameObject);
                        if (!Arrow.IsArrow)
                        {
                            transform.position = new Vector3(-9999f, -9999f, 0f);
                            RockThrow.OnRelease(this.gameObject);
                        }

                        break;
                    
                }
            }
            Destructibles.Destructibles[] spawned  = Object.FindObjectsByType<Destructibles.Destructibles>();
                foreach (Destructibles.Destructibles obj in spawned)
                {
                    if (myBounds.Intersects(obj.MyBounds))
                    {
                        DestructiblesSpawner.Instance.OnRelease(obj.gameObject);
                        if (!Arrow.IsArrow)
                        {
                            transform.position = new Vector3(-9999f, -9999f, 0f);
                            RockThrow.OnRelease(this.gameObject); 
                            
                        }
                    }
                    
                }

                if (Arrow.IsArrow)
                {
                    _spriteRenderer.sprite = newSprite;
                }
                else
                {
                    _spriteRenderer.sprite = _originalSprite;
                }

            transform.Translate(Vector3.up * (Time.deltaTime *10f));
        }
    

    }
}
