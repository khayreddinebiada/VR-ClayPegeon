using UnityEngine;

namespace game.manager
{
    public class AudioManager : MonoBehaviour
    {
        public enum AudioType
        {
            Music,
            Sound
        };

        #region Music
        public static bool musicSilence = false;

        public void PauseMusic()
        {
            musicSilence = true;
        }
        public void UnpauseMusic()
        {
            musicSilence = false;
        }
        #endregion

        #region Sound
        public static bool soundSilence = false;

        public void PauseSound()
        {
            soundSilence = true;
        }
        public void UnpauseSound()
        {
            soundSilence = false;
        }
        #endregion
    }

}