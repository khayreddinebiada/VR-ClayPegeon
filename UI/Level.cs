using UnityEngine;
using UnityEngine.UI;

namespace game.ui
{
    public class Level : MonoBehaviour
    {
        public int levelIndex;
        public bool levelLocked;
        public int starsNumber;

        [SerializeField]
        private Button levelButton;
        [SerializeField]
        private Image LockImage;
        [SerializeField]
        private Text textStarsContent;
        [SerializeField]
        private Text textLevelIndex;

        // Start is called before the first frame update
        void Start()
        {
            if (levelLocked)
            {
                levelButton.enabled = false;
                LockImage.gameObject.SetActive(true);
            }
            else
            {
                levelButton.enabled = true;
                LockImage.gameObject.SetActive(false);
            }

            textStarsContent.text = "Stars: " + starsNumber;
            textLevelIndex.text = "Level: " + levelIndex;
        }

    }
}