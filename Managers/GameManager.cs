using game.control;
using game.data;
using game.ui;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace game.manager
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;

        [Header("Components")]
        [SerializeField]
        private ControllerGun _controllerGun;
        public ControllerGun controllerGun
        {
            get { return _controllerGun; }
        }

        [SerializeField]
        private Transform gunPosition;
        [SerializeField]
        private Transform gunParent;

        [SerializeField]
        private TargetManager _targetManager;
        public TargetManager targetManager
        {
            get { return _targetManager; }
        }

        [Header("Win and Loss")]
        [SerializeField]
        private int minPointsFor2Stars = 20;
        [SerializeField]
        private int minPointsFor3Stars = 30;
        [SerializeField]
        private UnityEvent onWin;
        [SerializeField]
        private UnityEvent onLost;

        [Header("Bullets and Coin")]
        [SerializeField]
        private int _bulletOnLevel = 5;
        [SerializeField]
        private int _scaleTimeCoin = 1;
        [SerializeField]
        private int _scaleStarCoin = 10;

        private int _coinsOnThisLevel = 0;
        private bool _isPaused = false;
        private bool _gameEnd = false;
        private float _time;
        

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            // if no gun in the level we will instance one that used from player.
            if(controllerGun == null)
            {
                _controllerGun = Instantiate(GlobalData.instance.gunInfos[GlobalData.GetIndexCurrentGun()].prefabOnPlay, gunPosition.position, gunPosition.rotation, gunParent).GetComponent<ControllerGun>();
            }
            _controllerGun.gun.gunContain = _bulletOnLevel;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isPaused && _gameEnd)
                return;

            _time += Time.deltaTime;
            if (controllerGun.gun.gunContain == 0 && controllerGun.gun.maxSavingBullets == 0)
            {
                EndGame();
            }
        }

        public void EndGame()
        {
            _gameEnd = true;
            int starsNumber;

            if (minPointsFor3Stars <= targetManager.GetTotalPoints())
            {
                starsNumber = 3;
                _coinsOnThisLevel = CalculateCoin(starsNumber);
                MainCanvasManager.instance.PlayerWin((int)_time, _coinsOnThisLevel, targetManager.GetTotalPoints());
                onWin.Invoke();
            }
            else
            {
                if (minPointsFor2Stars <= targetManager.GetTotalPoints())
                {
                    starsNumber = 2;
                    _coinsOnThisLevel = CalculateCoin(starsNumber);
                    MainCanvasManager.instance.PlayerWin((int)_time, _coinsOnThisLevel, targetManager.GetTotalPoints());
                    onWin.Invoke();
                }
                else
                {
                    starsNumber = 1;
                    _coinsOnThisLevel = CalculateCoin(starsNumber);
                    MainCanvasManager.instance.PlayerLost((int)_time, _coinsOnThisLevel, targetManager.GetTotalPoints());
                    onLost.Invoke();
                }
            }

            StartCoroutine(WaitAndMakeAnimation(starsNumber));
        }

        private int CalculateCoin(int stars)
        {
            int coins = (60 - (int)_time) * _scaleTimeCoin + stars * _scaleStarCoin;
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
            return _isPaused;
        }

        public void MakePause()
        {
            _isPaused = false;
        }

        public void MakeUnpause()
        {
            _isPaused = true;
        }

        public float GetCurrentTime()
        {
            return _time;
        }
    }
}
