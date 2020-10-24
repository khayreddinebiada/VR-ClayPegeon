using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public int TotalCoins()
    {
        return PlayerPrefs.GetInt("Coins");
    }

    public void AddCoins(int coins)
    {
        PlayerPrefs.SetInt("Coins", TotalCoins() + coins);
    }

    public int GetIndexStopLevelOn(int indexEnvironment)
    {
        return PlayerPrefs.GetInt("Stop Level On" + indexEnvironment);
    }

    public void SetIndexStopLevelOn(int indexEnvironment, int indexLevel)
    {
        PlayerPrefs.SetInt("Stop Level On" + indexEnvironment, indexLevel);
    }

    public void SetStarsOnLevel(int indexEnvironment, int indexLevel, int newValue)
    {
        if (newValue <= GetStarsOnLevel(indexEnvironment, indexLevel))
            return;

        PlayerPrefs.SetInt("StarsOnLevel E:" + indexEnvironment + " L: " + indexLevel, newValue);
    }

    public int GetStarsOnLevel(int indexEnvironment, int indexLevel)
    {
        return PlayerPrefs.GetInt("StarsOnLevel E:" + indexEnvironment + " L: " + indexLevel);
    }

    public int GetCurrentGunUsed()
    {
        return PlayerPrefs.GetInt("Get Current Gun Used");
    }

    public void SetCurrentGunUsed(int indexGun)
    {
        PlayerPrefs.SetInt("Get Current Gun Used", indexGun);
    }


    public bool IsItUnlockedThisGun(int indexGun)
    {
        if (indexGun == 0) // If Index of first gun we will use it.
            return true;

        return PlayerPrefs.HasKey("Gun Unlocked" + indexGun);
    }

    public void UnlockThisGun(int indexGun)
    {
        PlayerPrefs.SetInt("Gun Unlocked" + indexGun, 1);
    }
}
