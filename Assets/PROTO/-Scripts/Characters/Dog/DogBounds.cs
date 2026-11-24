
using UnityEngine;
using Object = UnityEngine.Object;

namespace PROTO._Scripts.Characters.Dog
{
    public class DogBounds : MonoBehaviour
    {
        [SerializeField] private Vector3 boundsSize = new (.9f, .6f,1f);
        public Vector3 boundsCenter;
        public Bounds dogBounds;
        

        private void OnDrawGizmos() 
        {
       
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(dogBounds.center, dogBounds.size);

        }
        private void Update()
        {
            
            boundsCenter = transform.position;
            dogBounds = new Bounds(boundsCenter, boundsSize);
      
        }
 
    }
}
