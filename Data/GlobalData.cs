using System;
using game.target;
using UnityEngine;

namespace game.data
{
    public class GlobalData : MonoBehaviour
    {
        #region Variables 
        private static GlobalData _instance;
        public static GlobalData instance
        {
            get { return _instance; }
        }

        [SerializeField]
        private GunInfo[] _gunInfos;
        public GunInfo[] gunInfos
        {
            get { return _gunInfos; }
        }

        public GunInfo GetCurrentGunInfo()
        {
            return gunInfos[GetIndexCurrentGun()];
        }

        private void Awake()
        {
            _instance = this;
        }
        #endregion
        #region coin
        [SerializeField]
        private static int initCoinGive = 10000;
        public static int TotalCoins()
        {
            if(!PlayerPrefs.HasKey("Coins"))
                PlayerPrefs.SetInt("Coins", initCoinGive);

            return PlayerPrefs.GetInt("Coins");
        }

        public static void AddCoins(int coins)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins() + coins);
        }
        public static void MinusCoin(int price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins() - price);
        }
        #endregion
        #region Levels
        private static int _totalLevels = 30;
        public static int GetTotalLevels()
        {
            return _totalLevels;
        }
        public static int GetTotalLevels(int indexEnv)
        {
            switch (indexEnv)
            {
                case 0:
                    return 10;
                case 1:
                    return 10;
                case 2:
                    return 10;
                    
            }

            return 0;
        }

        public static void MakeCompleteEnvironment(int index)
        {
            PlayerPrefs.SetInt("Environment Completed" + index, 1);
        }

        public static bool EnvironmentIsCompleted(int index)
        {
            return PlayerPrefs.HasKey("Environment Completed" + index);
        }

        public static int GetTotalStars()
        {
            return PlayerPrefs.GetInt("Total Stars");
        }

        public static void SetLevelPlayOn(int indexEnvironment, int indexLevel)
        {
            PlayerPrefs.SetInt("Level Play On", indexLevel);
            PlayerPrefs.SetInt("Environment Play On", indexEnvironment);
        }

        public static int GetLevelPlayOn()
        {
            return PlayerPrefs.GetInt("Level Play On");
        }
        public static int GetEnvironmentPlayOn()
        {
            return PlayerPrefs.GetInt("Environment Play On");
        }

        private static void AddStarsOnTotal(int stars)
        {
            PlayerPrefs.SetInt("Total Stars", GetTotalStars() + stars);
        }

        public static int GetTotalLevelsWin()
        {
            return PlayerPrefs.GetInt("Total Level Win");
        }

        public static void AddNewLevelInWinList()
        {
            PlayerPrefs.SetInt("Total Level Win", GetTotalLevelsWin() + 1);
        }

        public static int GetTotalLevelsWinOnEnv(int indexEnvironment)
        {
            return PlayerPrefs.GetInt("Total Level Win Env E:" + indexEnvironment);
        }

        public static void AddNewLevelInWinListOnEnv(int indexEnvironment)
        {
            int tw = GetTotalLevelsWin();
            if (GetTotalLevels(indexEnvironment) == tw)
            {
                MakeCompleteEnvironment(indexEnvironment);
                return;
            }
            PlayerPrefs.SetInt("Total Level Win Env E:" + indexEnvironment, tw + 1);
        }

        public static void SetStarsOnLevel(int indexEnvironment, int indexLevel, int newValue)
        {
            int oldValue = GetStarsOnLevel(indexEnvironment, indexLevel);

            if (1 < newValue && oldValue <= 1)
            {
                AddNewLevelInWinList();
                AddNewLevelInWinListOnEnv(indexEnvironment);
            }

            if (newValue <= oldValue)
                return;

            AddStarsOnTotal(newValue - oldValue);
            PlayerPrefs.SetInt("StarsOnLevel E:" + indexEnvironment + " L: " + indexLevel, newValue - oldValue);
        }

        public static int GetStarsOnLevel(int indexEnvironment, int indexLevel)
        {
            return PlayerPrefs.GetInt("StarsOnLevel E:" + indexEnvironment + " L: " + indexLevel);
        }
        #endregion
        #region Guns
        public static bool WeHaveThisGun(int indexGun)
        {
            if (indexGun == 0 || indexGun == 1) // If Index of first gun we will use it.
                return true;

            return PlayerPrefs.HasKey("Gun Unlocked" + indexGun);
        }
        public static void TakeThisGun(int indexGun)
        {
            PlayerPrefs.SetInt("Gun Unlocked" + indexGun, 1);
        }

        public static int GunNumber()
        {
            return 19;
        }

        private static int defaultGunIndex = 0;
        public static int GetIndexCurrentGun()
        {
            if (!PlayerPrefs.HasKey("Current Gun"))
                PlayerPrefs.SetInt("Current Gun", defaultGunIndex);

            return PlayerPrefs.GetInt("Current Gun");
        }

        public static void ChangeIndexCurrentGun(int newUsed)
        {
            PlayerPrefs.SetInt("Current Gun", newUsed);
        }
        #endregion
    }
}