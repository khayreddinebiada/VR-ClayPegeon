using System;
using game.objects;
using UnityEngine;

namespace game.data
{
    public static class GlobalData
    {

        public static int TotalCoins()
        {
            return PlayerPrefs.GetInt("Coins");
        }

        public static void AddCoins(int coins)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins() + coins);
        }

        public static int GetIndexStopLevelOn(int indexEnvironment)
        {
            return PlayerPrefs.GetInt("Stop Level On" + indexEnvironment);
        }

        public static int GetIndexLevelsWin()
        {
            return PlayerPrefs.GetInt("Total Level Win");
        }

        public static void LevelIsWinOnEnvir(int indexEnvironment)
        {
            PlayerPrefs.SetInt("Total Level Win", PlayerPrefs.GetInt("Total Level Win") + 1);
            PlayerPrefs.SetInt("Stop Level On" + indexEnvironment, PlayerPrefs.GetInt("Stop Level On" + indexEnvironment) + 1);
        }

        public static void SetStarsOnLevel(int indexEnvironment, int indexLevel, int newValue)
        {
            if (newValue <= GetStarsOnLevel(indexEnvironment, indexLevel))
                return;

            PlayerPrefs.SetInt("StarsOnLevel E:" + indexEnvironment + " L: " + indexLevel, newValue);
        }

        public static int GetStarsOnLevel(int indexEnvironment, int indexLevel)
        {
            return PlayerPrefs.GetInt("StarsOnLevel E:" + indexEnvironment + " L: " + indexLevel);
        }

        public static void BuyGun(int gunIndex, int price)
        {
            UnlockThisGun(gunIndex);
            MinusPrice(price);
        }
        public static void MinusPrice(int price)
        {
            PlayerPrefs.SetInt("Coins", TotalCoins() - price);
        }
        public static int GetCurrentGunUsed()
        {
            return PlayerPrefs.GetInt("Get Current Gun Used");
        }

        public static void SetCurrentGunUsed(int indexGun)
        {
            PlayerPrefs.SetInt("Get Current Gun Used", indexGun);
        }

        public static bool IsItUnlockedThisGun(int indexGun)
        {
            if (indexGun == 0 || indexGun == 1) // If Index of first gun we will use it.
                return true;

            return PlayerPrefs.HasKey("Gun Unlocked" + indexGun);
        }

        public static void UnlockThisGun(int indexGun)
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
    }
}