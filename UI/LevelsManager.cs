using game.data;
using UnityEngine;

namespace game.ui
{
    public class LevelsManager : MonoBehaviour
    {

        [SerializeField]
        private Environment environment;
        [SerializeField]
        private GameObject prefabLevel;
        [SerializeField]
        private Transform content;

        private int _levelsNumber;
        private Level[] levels;


        // Start is called before the first frame update
        private void Start()
        {
            int levelsWin = GlobalData.GetTotalLevelsWinOnEnv(environment.indexEnvironment);
            print("Level: " + levelsWin + ", Environment: " + environment.indexEnvironment);
            _levelsNumber = GlobalData.GetTotalLevels(environment.indexEnvironment);
            levels = new Level[_levelsNumber];
            for (int i = 0; i < _levelsNumber; i++)
            {
                levels[i] = Instantiate(prefabLevel, content).GetComponent<Level>();
                levels[i].levelIndex = i + 1;
                levels[i].starsNumber = GlobalData.GetStarsOnLevel(environment.indexEnvironment, i);

                if (i <= levelsWin)
                {
                    levels[i].levelLocked = false;
                }
                else
                {
                    levels[i].levelLocked = true;
                }
            }
        }
    }
}
