using PROTO._Scripts.Characters.Cat;
using UnityEngine;

namespace PROTO._Scripts.Characters.Npc
{
    public class Cow:NpcBounds
    {
        private void Update()
        {
            
            if (npcBounds.Intersects(PlayerBounds.MyBounds))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (PlayerUI.NumberOfHay == 5)
                    {
                        Destructibles.Destructibles.IsQuittingOrInitializing = true;
                        PlayerEvent.Win = true;
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