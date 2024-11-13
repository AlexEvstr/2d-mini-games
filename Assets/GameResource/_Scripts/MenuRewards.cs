using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class MenuRewards : MonoBehaviour
{
    public Text totalGoldText, totalEnergyText, totalGem1Text, totalGem2Text, totalGem3Text, totalGem4Text, totalLifeText, totalMoneyText;
    private int totalGold, totalEnergy, totalGem1, totalGem2, totalGem3, totalGem4, totalLife, totalMoney;
    private int previousGold, previousEnergy, previousGem1, previousGem2, previousGem3, previousGem4, previousLife, previousMoney;

    private string goldString = "totalGold";
    private string energyString = "totalEnergy";
    private string gem1String = "totalGem1";
    private string gem2String = "totalGem2";
    private string gem3String = "totalGem3";
    private string gem4String = "totalGem4";
    private string lifeString = "totalLife";
    private string moneyString = "totalMoney";

    private void Awake()
    {
        GetPlayerProgress();
    }

    private void GetPlayerProgress()
    {
        //Gold
        if (PlayerPrefs.HasKey(goldString))
        {
            totalGold = PlayerPrefs.GetInt(goldString);
        }
        else
        {
            totalGold = 99; //Default Gold Value
            PlayerPrefs.SetInt(goldString, totalGold);
            PlayerPrefs.Save();
        }
        //Energy
        if (PlayerPrefs.HasKey(energyString))
        {
            totalEnergy = PlayerPrefs.GetInt(energyString);
        }
        else
        {
            totalEnergy = 0;
            PlayerPrefs.SetInt(energyString, totalEnergy);
        }
        //Life
        if (PlayerPrefs.HasKey(lifeString))
        {
            totalLife = PlayerPrefs.GetInt(lifeString);
        }
        else
        {
            totalLife = 0;
            PlayerPrefs.SetInt(lifeString, totalLife);
        }
        //Money
        if (PlayerPrefs.HasKey(moneyString))
        {
            totalMoney = PlayerPrefs.GetInt(moneyString);
        }
        else
        {
            totalMoney = 0;
            PlayerPrefs.SetInt(moneyString, totalMoney);
        }
        //Gem1
        if (PlayerPrefs.HasKey(gem1String))
        {
            totalGem1 = PlayerPrefs.GetInt(gem1String);
        }
        else
        {
            totalGem1 = 0;
            PlayerPrefs.SetInt(gem1String, totalGem1);
        }
        //Gem2
        if (PlayerPrefs.HasKey(gem2String))
        {
            totalGem2 = PlayerPrefs.GetInt(gem2String);
        }
        else
        {
            totalGem2 = 0;
            PlayerPrefs.SetInt(gem2String, totalGem2);
        }
        //Gem3
        if (PlayerPrefs.HasKey(gem3String))
        {
            totalGem3 = PlayerPrefs.GetInt(gem3String);
        }
        else
        {
            totalGem3 = 0;
            PlayerPrefs.SetInt(gem3String, totalGem3);
        }
        //Gem4
        if (PlayerPrefs.HasKey(gem4String))
        {
            totalGem4 = PlayerPrefs.GetInt(gem4String);
        }
        else
        {
            totalGem4 = 0;
            PlayerPrefs.SetInt(gem4String, totalGem4);
        }

        totalGem1 = 600;
        totalGem2 = 600;
        totalGem3 = 600;
        totalGem4 = 600;

        PlayerPrefs.Save();
        StartCoroutine(UpdateRewardsAmount());
    }

    private IEnumerator UpdateRewardsAmount()
    {
        // Animation for increasing and decreasing of coins amount
        const float seconds = 0.5f;
        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            totalGoldText.text = Mathf.Floor(Mathf.Lerp(previousGold, totalGold, (elapsedTime / seconds))).ToString();
            totalLifeText.text = Mathf.Floor(Mathf.Lerp(previousLife, totalLife, (elapsedTime / seconds))).ToString();
            totalGem1Text.text = Mathf.Floor(Mathf.Lerp(previousGem1, totalGem1, (elapsedTime / seconds))).ToString();
            totalGem2Text.text = Mathf.Floor(Mathf.Lerp(previousGem2, totalGem2, (elapsedTime / seconds))).ToString();
            totalGem3Text.text = Mathf.Floor(Mathf.Lerp(previousGem3, totalGem3, (elapsedTime / seconds))).ToString();
            totalGem4Text.text = Mathf.Floor(Mathf.Lerp(previousGem4, totalGem4, (elapsedTime / seconds))).ToString();
            totalEnergyText.text = Mathf.Floor(Mathf.Lerp(previousEnergy, totalEnergy, (elapsedTime / seconds))).ToString();
            totalMoneyText.text = Mathf.Floor(Mathf.Lerp(previousMoney, totalMoney, (elapsedTime / seconds))).ToString();

            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        previousGold = totalGold;
        previousLife = totalLife;
        previousGem1 = totalGem1;
        previousGem2 = totalGem2;
        previousGem3 = totalGem3;
        previousGem4 = totalGem4;
        previousEnergy = totalEnergy;
        previousMoney = totalMoney;

        totalGoldText.text = totalGold.ToString();
        totalLifeText.text = totalLife.ToString();
        totalGem1Text.text = totalGem1.ToString();
        totalGem2Text.text = totalGem2.ToString();
        totalGem3Text.text = totalGem3.ToString();
        totalGem4Text.text = totalGem4.ToString();
        totalEnergyText.text = totalEnergy.ToString();
        totalMoneyText.text = totalMoney.ToString();

        SavePlayerProgress(goldString, totalGold);
        SavePlayerProgress(energyString, totalEnergy);
        SavePlayerProgress(gem1String, totalGem1);
        SavePlayerProgress(gem2String, totalGem2);
        SavePlayerProgress(gem3String, totalGem3);
        SavePlayerProgress(gem4String, totalGem4);
        SavePlayerProgress(lifeString, totalLife);
        SavePlayerProgress(moneyString, totalMoney);
    }

    private void SavePlayerProgress(string st, int value)
    {
        PlayerPrefs.SetInt(st, value);
        PlayerPrefs.Save();
    }
}