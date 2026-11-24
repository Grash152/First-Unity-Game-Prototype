using PROTO._Scripts.Characters.Cat;
using UnityEngine;

namespace PROTO._Scripts.Characters.Dog
{
    public class DogMove : MonoBehaviour
    {
        [SerializeField]  public bool isGuarding = false;
        private Vector3 _guardSpot;
        void Start()
        {
            _guardSpot = transform.position;
        }
        private void Update()
        {
            
            if (GuardDog.HomingBounds.Intersects(PlayerBounds.MyBounds)&& isGuarding)
            {

                    Vector3 direction = PlayerBounds.MyBounds.center - transform.position;

                    transform.Translate(direction.normalized * (Time.deltaTime * 2.5f));
            }
            else if (isGuarding)
            {
                Vector3 direction = _guardSpot - transform.position;
                transform.Translate(direction.normalized * (Time.deltaTime * 2.5f));
            }
            else if (!isGuarding)
            {
                transform.Translate(Vector3.up * (Time.deltaTime *10f));
            }
        }
    }
}