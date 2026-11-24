using UnityEngine;

namespace PROTO._Scripts.Collectibles
{
    public class CollectiblesBounds: MonoBehaviour
    {
        [SerializeField] protected Vector3 boundsSize = new Vector3(0.3f, 0.3f,1f);
        public Vector3 boundsCenter = Vector3.zero;
        public Bounds myBounds;
       private void Update()
        {
            myBounds = new Bounds(boundsCenter, boundsSize);
            boundsCenter = transform.position;
        }
    
        private void OnDrawGizmos() 
        {
 
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(myBounds.center, myBounds.size);
        }
    }
}