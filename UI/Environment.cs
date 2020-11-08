using game.data;
using UnityEngine;
using UnityEngine.UI;

namespace game.ui
{
    public class Environment : MonoBehaviour
    {
        [SerializeField]
        private int indexEnvironment = 0;
        [SerializeField]
        private int minStarsForUnlock = 0;

        private bool isUnlock;
        [SerializeField]
        private Image lockImage;
        [SerializeField]
        private Button button;
        // Start is called before the first frame update
        private void Start()
        {
            if(minStarsForUnlock <= GlobalData.GetTotalStars())
            {
                isUnlock = true;
                button.enabled = false;
                lockImage.gameObject.SetActive(true);
            }
        }

    }
}
