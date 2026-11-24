using PROTO._Scripts.Characters.Cat;
using PROTO._Scripts.Collectibles;
using UnityEngine;

namespace PROTO._Scripts.Characters.Npc
{
    public class Man : NpcBounds
    {
        private void Update()
        {
           
            if (npcBounds.Intersects(PlayerBounds.MyBounds))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (Coin.NumberOfCoins == 5)
                    {
                        PlayerUI.NumberOfHay++;
                        Coin.NumberOfCoins = 0;
                    }
                }
                dialogueCanvas.SetActive(true);
            }
            else
            {
                dialogueCanvas.SetActive(false);
            }
        }
    }
}