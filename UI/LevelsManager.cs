using game.data;
using UnityEngine;

namespace game.ui
{
    public class LevelsManager : MonoBehaviour
    {
        [SerializeField]
        private int currentEnvironment;
        [SerializeField]
        private int LevelNumber;
        private Level[] levels;

        [SerializeField]
        private GameObject prefabLevel;
        [SerializeField]
        private Transform content;

        // Start is called before the first frame update
        private void Start()
        {
            int levelsWin = GlobalData.GetTotalLevelsWinOnEnv(currentEnvironment);

            levels = new Level[LevelNumber];

            for (int i = 0; i < LevelNumber; i++)
            {
                levels[i] = Instantiate(prefabLevel, content).GetComponent<Level>();
                levels[i].starsNumber = GlobalData.GetStarsOnLevel(currentEnvironment, i);

                if (i <= levelsWin)
                {
                    levels[i].levelUnlock = true;
                }
                else
                {
                    levels[i].levelUnlock = false;
                }
            }
        }
    }
}
