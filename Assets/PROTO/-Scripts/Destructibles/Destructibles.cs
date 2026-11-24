using UnityEngine;

namespace PROTO._Scripts.Destructibles
{
    public class Destructibles : MonoBehaviour
    {
        [SerializeField] private Vector3 boundsSize = new Vector3(0.7f, 0.5f,1f);
        public bool noDrops;
        public static bool IsQuittingOrInitializing = true;
        public Bounds MyBounds => new(transform.position, boundsSize);
        protected virtual void OnEnable()
        {
            noDrops = false; 
        }

        protected virtual void DropItem(){}
  
        
 
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, boundsSize);
        }
        
    }
}
