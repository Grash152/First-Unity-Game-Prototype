using UnityEngine;

namespace PROTO._Scripts.Characters.Cat
{
    public class PlayerBounds : MonoBehaviour
    {
        [SerializeField] protected Vector3 boundsSize = new Vector3(0.7f, 0.5f, 1f);
        public static Bounds FutureBounds { get;private set; }
        public static Bounds MyBounds { get;private set; }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(MyBounds.center, MyBounds.size);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(FutureBounds.center, MyBounds.size);
        }

        private void Update()
        {
            Vector3 targetPosition = transform.position + PlayerMove.Velocity * Time.deltaTime;
            FutureBounds = new Bounds(targetPosition, boundsSize);
            
            Vector3 actualBoundsCenter = transform.position;
            MyBounds = new Bounds(actualBoundsCenter, boundsSize);
        }
        
    }
}
