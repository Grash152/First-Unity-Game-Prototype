
using PROTO._Scripts.Characters.Cat;
using UnityEngine;

namespace PROTO._Scripts.Collectibles
{
    public class Coin : CollectiblesBounds
    {
        public static int NumberOfCoins;
        void Update()
        {
            boundsCenter = transform.position; //mise à jour
            myBounds = new Bounds(boundsCenter, boundsSize);
           
                if (myBounds.Intersects(PlayerBounds.MyBounds))
                {
                    if (NumberOfCoins < 5)
                        NumberOfCoins++;
                    Destroy(this.gameObject);
                }

                if (PlayerEvent.Lose || PlayerEvent.Win)
                {
                    Destroy(this.gameObject);
                }
            

        }

        void OnDisable()
        {
            Destroy(this.gameObject);
        }
    }
}
