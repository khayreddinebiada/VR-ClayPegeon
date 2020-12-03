using game.control;
using game.data;
using game.ui;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace game.manager
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;
        [Header(" On Editor")]
        [SerializeField]
        private bool _modeTest = false;
        [SerializeField]
        private int _selectLevelForTest = 0;

        [Header("Levels Info")]
        [SerializeField]
        private GameObject[] prefabLevels;
        private GameObject currentLevelPrefab;
        private int _currentLevel = 0;
        [SerializeField]
        private bool isLastEnvironment = false;
        [SerializeField]
        private int _currentEnvironment = 0;
        [SerializeField]
        private int _bonusAmount = 1500;

        [Header("Components")]
        [SerializeField]
        private Transform numbers;
        [SerializeField]
        private Transform gunPosition;
        [SerializeField]
        private Transform gunParent;

        private ControllerGun _controllerGun;
        public ControllerGun controllerGun
        {
            get { return _controllerGun; }
        }


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


        private int _coinsOnThisLevel = 0;
        private bool _isPaused = false;
        private bool _gameEnd = false;
        private float _time;
        

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
            _currentLevel = GlobalData.GetLevelPlayOn();
            if (_modeTest)
            {
                currentLevelPrefab = Instantiate(prefabLevels[_selectLevelForTest]);
            }
            else
            {
                currentLevelPrefab = Instantiate(prefabLevels[_currentLevel]);
            }
            _targetManager = currentLevelPrefab.GetComponent<TargetManager>();
            _targetManager.numbers = numbers;
        }

        private void Start()
        {
            InitializeGun();
        }

        private void InitializeGun()
        {
            if (controllerGun == null)
            {
                _controllerGun = Instantiate(GlobalData.instance.gunInfos[GlobalData.GetIndexCurrentGun()].prefabOnPlay, gunPosition.position, gunPosition.rotation, gunParent).GetComponent<ControllerGun>();
            }
            _controllerGun.gun.gunContain = _targetManager.bulletOnLevel;
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
            if (_gameEnd)
                return;

            _gameEnd = true;
            int starsNumber;

            if (minPointsFor3Stars <= targetManager.GetTotalPoints())
            {
                starsNumber = 3;
                WinLevel(starsNumber);
            }
            else
            {
                if (minPointsFor2Stars <= targetManager.GetTotalPoints())
                {
                    starsNumber = 2;
                    WinLevel(starsNumber);
                }
                else
                {
                    starsNumber = 1;
                    LostLevel();
                }
            }

            GlobalData.SetStarsOnLevel(_currentEnvironment, _currentLevel, starsNumber);
            StartCoroutine(WaitAndMakeAnimation(starsNumber));
        }

        private void WinLevel(int starsNumber)
        {
            if(IsLastLevel())
            {
                MainCanvasManager.instance.newEnvironmentPanel.SetActive(true);
                AddBonus(_bonusAmount);
            }
            _coinsOnThisLevel = CalculateCoin(starsNumber);
            MainCanvasManager.instance.PlayerWin((int)_time, _coinsOnThisLevel, targetManager.GetTotalPoints());
            onWin.Invoke();
        }

        private void AddBonus(int bonusAmount)
        {
            MainCanvasManager.instance.AddBonus(bonusAmount);
            GlobalData.AddCoins(bonusAmount);
        }

        private void LostLevel()
        {
            _coinsOnThisLevel = CalculateCoin(1);
            MainCanvasManager.instance.PlayerLost((int)_time, _coinsOnThisLevel, targetManager.GetTotalPoints());
            onLost.Invoke();
        }

        public void GoToTheNextLevel()
        {
            if (IsLastLevel())
            {
                if (!isLastEnvironment)
                {
                    GlobalData.SetLevelPlayOn(_currentEnvironment, 1);
                    SceneManager.LoadSceneAsync("Level" + (_currentEnvironment + 2));
                }
                else
                {
                    SceneManager.LoadSceneAsync("Game finished");
                }
            }
            else
            {
                GlobalData.SetLevelPlayOn(_currentEnvironment, _currentLevel + 1);
                SceneManager.LoadSceneAsync("Level" + (_currentEnvironment + 1));
            }

        }

        public void Replay()
        {
            GlobalData.SetLevelPlayOn(_currentEnvironment, _currentLevel);
            SceneManager.LoadSceneAsync("Level" + (_currentEnvironment + 1));
        }

        public void GoMenu()
        {
            SceneManager.LoadSceneAsync("Menu");
        }

        public bool IsLastLevel()
        {
            return prefabLevels.Length -1 <= _currentLevel;
        }

        private int CalculateCoin(int stars)
        {
            int coins = (60 - (int)_time) * _targetManager.scaleTimeCoin + stars * _targetManager.scaleStarCoin;
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
