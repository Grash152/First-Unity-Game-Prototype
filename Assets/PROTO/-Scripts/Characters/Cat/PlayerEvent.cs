using System;
using PROTO._Scripts.Characters.Dog;
using PROTO._Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace PROTO._Scripts.Characters.Cat
{
    public class PlayerEvent : MonoBehaviour
    {
        public static bool Win;
        public static bool Lose = false;
        
        private void Awake()
        {
            Win = false;
        }

        private void Update()
        {
            if (PlayerUI.Invincible < PlayerUI.InvincibleDuration)
            {
                PlayerUI.Invincible += Time.deltaTime;
            }

            DogBounds[] allEnemies = Object.FindObjectsByType<DogBounds>();
            GuardDog guard = Object.FindAnyObjectByType<GuardDog>();
            foreach (DogBounds enemy in allEnemies)
            {
                if (enemy != null)
                {
                    if (PlayerBounds.MyBounds.Intersects(enemy.dogBounds)  && PlayerUI.Invincible - PlayerUI.InvincibleDuration >= 0)
                    {
                        PlayerStats.Instance.TakeDamage(1f);
                        PlayerUI.Invincible = 0f;
                    }
                }

                if (guard != null)
                {
                    if ( PlayerBounds.MyBounds.Intersects(guard.dogBounds) && PlayerUI.Invincible - PlayerUI.InvincibleDuration >= 0)
                    {
                        PlayerStats.Instance.TakeDamage(1f);
                        PlayerUI.Invincible = 0f;
                    } 
                }
            }
            if (Win)
            {
                SceneManager.LoadScene("PROTO_End");
            }

            if (Lose)
            {
                SceneManager.LoadScene("PROTO_EndLost");
            }
        }
    }
}
