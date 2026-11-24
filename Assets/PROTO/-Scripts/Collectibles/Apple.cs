using PROTO._Scripts.Characters;
using PROTO._Scripts.Characters.Cat;
using PROTO._Scripts.UI;
using UnityEngine;

namespace PROTO._Scripts.Collectibles
{
    public class Apple : CollectiblesBounds
    {
        private void Update()
        {
            boundsCenter = transform.position; 
            myBounds = new Bounds(boundsCenter, boundsSize);
          
                if (myBounds.Intersects(PlayerBounds.MyBounds))
                {
                    PlayerStats.Instance.Heal(1f);
                    Destroy(this.gameObject);
                }

                if (PlayerEvent.Lose || PlayerEvent.Win)
                {
                    Destroy(this.gameObject);
                }

            
        }
        private void OnDisable()
        {
            Destroy(this.gameObject);
        }
    }
}
