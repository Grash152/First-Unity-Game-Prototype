using UnityEngine;
using UnityEngine.SceneManagement;

namespace PROTO._Scripts.Scene_Management
{
    public class VideoButton : MonoBehaviour
    {
        public void Video()
        {
        SceneManager.LoadScene("PROTO_Video");
        }
    }
}
