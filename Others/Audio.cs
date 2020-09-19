using UnityEngine;
using game.manager;
namespace game.others
{
    [RequireComponent(typeof(AudioSource))]
    public class Audio : MonoBehaviour
    {
        [SerializeField]
        private AudioManager.AudioType audioType = AudioManager.AudioType.Sound;

        private AudioSource _audioSource;

        // Start is called before the first frame update
        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            switch (audioType)
            {
                case AudioManager.AudioType.Sound:
                    if (!AudioManager.soundSilence)
                    {
                        _audioSource.Play();
                    }
                    break;
                case AudioManager.AudioType.Music:
                    if (!AudioManager.musicSilence)
                    {
                        _audioSource.Play();
                    }
                    break;
            }
        }
    }
}
