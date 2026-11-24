using PROTO._Scripts.Characters.Cat;
using UnityEngine;
namespace PROTO._Scripts.Collectibles
{
    public class Arrow : CollectiblesBounds
    {
        public static bool IsArrow = false;
        private void Awake()
        {
            IsArrow = false; 
        }
        void Update()
        {
            boundsCenter = transform.position; 
            myBounds = new Bounds(boundsCenter, boundsSize);
           
            if (myBounds.Intersects(PlayerBounds.MyBounds))
            {
                IsArrow = true;
                Destroy(this.gameObject);
            }

            if (PlayerEvent.Lose || PlayerEvent.Win)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
