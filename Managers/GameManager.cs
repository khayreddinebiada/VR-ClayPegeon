using UnityEngine;
using game.control;
using System.Collections;
using UnityEngine.Events;
using game.ui;
using game.data;

namespace game.manager
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;
        public ControllerGun controllerGun;
        public TargetManager targetManager;

        [SerializeField]
        private int minPointsFor2Stars = 20;
        [SerializeField]
        private int minPointsFor3Stars = 30;
        [SerializeField]
        private UnityEvent onWin;
        [SerializeField]
        private UnityEvent onLost;

        [SerializeField]
        private int scaleTimeCoin = 1;
        [SerializeField]
        private int scaleStarCoin = 10;

        private int coinsOnThisLevel = 0;
        private bool isPaused = false;
        private bool gameEnd = false;
        private float time;
        

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isPaused && gameEnd)
                return;

            time += Time.deltaTime;
            if (controllerGun.gun.gunContain == 0 && controllerGun.gun.maxSavingBullets == 0)
            {
                EndGame();
            }
        }

        public void EndGame()
        {
            gameEnd = true;
            int starsNumber;

            if (minPointsFor3Stars <= targetManager.GetTotalPoints())
            {
                starsNumber = 3;
                coinsOnThisLevel = CalculateCoin(starsNumber);
                MainCanvasManager.instance.PlayerWin((int)time, coinsOnThisLevel, targetManager.GetTotalPoints());
                onWin.Invoke();
            }
            else
            {
                if (minPointsFor2Stars <= targetManager.GetTotalPoints())
                {
                    starsNumber = 2;
                    coinsOnThisLevel = CalculateCoin(starsNumber);
                    MainCanvasManager.instance.PlayerWin((int)time, coinsOnThisLevel, targetManager.GetTotalPoints());
                    onWin.Invoke();
                }
                else
                {
                    starsNumber = 1;
                    coinsOnThisLevel = CalculateCoin(starsNumber);
                    MainCanvasManager.instance.PlayerLost((int)time, coinsOnThisLevel, targetManager.GetTotalPoints());
                    onLost.Invoke();
                }
            }

            StartCoroutine(WaitAndMakeAnimation(starsNumber));
        }

        private int CalculateCoin(int stars)
        {
            int coins = (60 - (int)time) * scaleTimeCoin + stars * scaleStarCoin;
            GlobalData.AddCoins(coins);
            return coins;
        }

        IEnumerator WaitAndMakeAnimation(int starNumber)
        {
            yield return new WaitForSeconds(1);
            if(1 < starNumber)
                MainCanvasManager.instance.MakeStars(starNumber);
        }

        public bool GameIsPaused()
        {
            return isPaused;
        }

        public void MakePause()
        {
            isPaused = false;
        }

        public void MakeUnpause()
        {
            isPaused = true;
        }

        public float GetCurrentTime()
        {
            return time;
        }
    }
}
