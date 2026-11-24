using PROTO._Scripts.Characters;
using PROTO._Scripts.Characters.Cat;
using PROTO._Scripts.Collectibles;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PROTO._Scripts.Scene_Management
{
    public class Start : MonoBehaviour
    {
        public void StartGame()
        {
            PlayerUI.Invincible = 3f;
            PlayerEvent.Lose = false;
            PlayerEvent.Win = false;
            PlayerUI.NumberOfHay = 0;
            Coin.NumberOfCoins = 0;
            SceneManager.LoadScene("PROTO");
        
        }
    }
}
