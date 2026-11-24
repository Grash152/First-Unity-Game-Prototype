using UnityEngine;

namespace PROTO._Scripts.UI
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        private void Start()
        {
            PlayMusic();
        }

        private void PlayMusic()
        {
            if (musicSource != null && !musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
        public void SetVolume(float value)
        {
        
            AudioListener.volume = value;
        }

    }
}
