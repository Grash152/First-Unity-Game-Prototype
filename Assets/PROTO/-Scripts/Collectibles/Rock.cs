using PROTO._Scripts.Characters;
using PROTO._Scripts.Characters.Cat;
using UnityEngine;

namespace PROTO._Scripts.Collectibles
{
    public class Rock : CollectiblesBounds
    {

        private void Update()
        {
            
            boundsCenter = transform.position; //mise à jour
            myBounds = new Bounds(boundsCenter, boundsSize);
         
                if (myBounds.Intersects(PlayerBounds.MyBounds))
                {
                    RockThrow.RockThrow.Bigger = true;
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
