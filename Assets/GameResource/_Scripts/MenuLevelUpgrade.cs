using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuLevelUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject[] _levels;
    private int _currentLevelIndex;
    private int totalGem1;
    private int totalGem2;
    private int totalGem3;
    private int totalGem4;
    public Text totalGem1Text, totalGem2Text, totalGem3Text, totalGem4Text;


    private void Start()
    {
        _currentLevelIndex = PlayerPrefs.GetInt("CurrentLvlIndex", 0);
        _levels[_currentLevelIndex].SetActive(true);

        totalGem1 = PlayerPrefs.GetInt("totalGem1", 0);
        totalGem2 = PlayerPrefs.GetInt("totalGem2", 0);
        totalGem3 = PlayerPrefs.GetInt("totalGem3", 0);
        totalGem4 = PlayerPrefs.GetInt("totalGem4", 0);
    }

    public void UpgradeFrom1To2Lvl()
    {
        if (totalGem1 >= 50)
        {
            _currentLevelIndex++;
            PlayerPrefs.SetInt("CurrentLvlIndex", _currentLevelIndex);
            totalGem1 -= 50;
            _levels[0].SetActive(false);
            _levels[1].SetActive(true);
            totalGem1Text.text = totalGem1.ToString();
            PlayerPrefs.SetInt("totalGem1", totalGem1);
        }
    }

    public void UpgradeFrom2To3Lvl()
    {
        if (totalGem2 >= 100)
        {
            _currentLevelIndex++;
            PlayerPrefs.SetInt("CurrentLvlIndex", _currentLevelIndex);
            totalGem2 -= 100;
            _levels[1].SetActive(false);
            _levels[2].SetActive(true);
            totalGem2Text.text = totalGem2.ToString();
            PlayerPrefs.SetInt("totalGem2", totalGem2);
        }
    }

    public void UpgradeFrom3To4Lvl()
    {
        if (totalGem3 >= 250)
        {
            _currentLevelIndex++;
            PlayerPrefs.SetInt("CurrentLvlIndex", _currentLevelIndex);
            totalGem3 -= 250; 
            _levels[2].SetActive(false);
            _levels[3].SetActive(true);
            totalGem3Text.text = totalGem3.ToString();
            PlayerPrefs.SetInt("totalGem3", totalGem3);
        }
        
    }

    public void UpgradeFrom4To5Lvl()
    {
        if (totalGem4 >= 500)
        {
            _currentLevelIndex++;
            PlayerPrefs.SetInt("CurrentLvlIndex", _currentLevelIndex);
            totalGem4 -= 500;
            _levels[3].SetActive(false);
            _levels[4].SetActive(true);
            totalGem4Text.text = totalGem4.ToString();
            PlayerPrefs.SetInt("totalGem4", totalGem4);
        }
    }
}