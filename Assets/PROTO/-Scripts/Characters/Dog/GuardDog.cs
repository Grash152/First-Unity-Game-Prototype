using System;
using PROTO._Scripts.RockThrow;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PROTO._Scripts.Characters.Dog
{
    public class GuardDog : MonoBehaviour
    {
        [SerializeField] private Vector3 boundsSize = new (.9f, .6f,1f);
        [SerializeField] private Vector3 homingSize = new (8f, 8f,1f);
        public Vector3 boundsCenter;
        public Bounds dogBounds;
        public static Bounds HomingBounds;
        [SerializeField] private GameObject prefab;
        private float _guardHp = 15f;
        public float invincible = 4f;
        private const float InvincibleDuration = .5f;

        
        private static SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            invincible = InvincibleDuration; 
        }
        private void OnDrawGizmos() 
        {
          
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(HomingBounds.center, dogBounds.size);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(dogBounds.center, dogBounds.size);

        }
        private void Update()
        {           
            if (invincible < InvincibleDuration)
            {
                invincible += Time.deltaTime;
                _spriteRenderer.color = Color.red;
            }
            else
                _spriteRenderer.color = Color.white;
            
            Projectile[] rocks = Object.FindObjectsByType<Projectile>();
            foreach (Projectile rock in rocks)
            {
                if (!gameObject.activeInHierarchy) break;
                if (dogBounds.Intersects(rock.myBounds)&&invincible >= InvincibleDuration)
                {
                    _guardHp--;
                  
                    if (_guardHp <= 0)
                    {
                        if(prefab != null )
                            Instantiate(prefab, transform.position, transform.rotation);
                        gameObject.SetActive(false);
                    }
                    invincible = 0f;
                    break;
                    
                }
            }
            boundsCenter = transform.position;
            dogBounds = new Bounds(boundsCenter, boundsSize); 
            HomingBounds = new Bounds(boundsCenter, homingSize);
      
        }
    
    }
}