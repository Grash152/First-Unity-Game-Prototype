using UnityEngine;
using UnityEngine.UI;

namespace PROTO._Scripts.UI
{
    public class VolumeSliderInit : MonoBehaviour
    {
        [SerializeField] private VolumeSlider audioManager;
        [SerializeField] private Slider volumeSlider;

        private void Awake()
        {
            volumeSlider.onValueChanged?.AddListener(audioManager.SetVolume);
        }
    }
}
