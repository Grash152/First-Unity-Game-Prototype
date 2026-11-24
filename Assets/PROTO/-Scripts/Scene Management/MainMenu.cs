using UnityEngine;
using UnityEngine.SceneManagement;

namespace PROTO._Scripts.Scene_Management
{
    public class MainMenu : MonoBehaviour
    {
        public void Menu()
        {
            SceneManager.LoadScene("PROTO_Intro");
        }
    }
}
