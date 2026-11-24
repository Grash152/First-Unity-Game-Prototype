using UnityEngine;

namespace PROTO._Scripts.Scene_Management
{
    public class QuitGame : MonoBehaviour
    {
        public void QuittingGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
