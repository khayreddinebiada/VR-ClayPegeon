using game.data;
using UnityEngine;
using UnityEngine.UI;

namespace game.ui
{
    public class Environment : MonoBehaviour
    {
        [SerializeField]
        private int _indexEnvironment = 0;
        public int indexEnvironment
        {
            get { return _indexEnvironment; }
        }

        [SerializeField]
        private int minStarsForUnlock = 0;

        private bool isLock;
        [SerializeField]
        private Image lockImage;
        [SerializeField]
        private Button button;

        [SerializeField]
        private LevelsManager _levelsManager;
        public LevelsManager levelsManager
        {
            get { return _levelsManager; }
        }

        // Start is called before the first frame update
        private void Start()
        {
            if(GlobalData.GetTotalStars() < minStarsForUnlock)
            {
                isLock = true;
                button.enabled = false;
                lockImage.gameObject.SetActive(true);
            }
        }
    }
}
