using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIButtons : MonoBehaviour
{
    [SerializeField] private GameObject _playClaw;
    [SerializeField] private GameObject _buyClaw;

    [SerializeField] private GameObject _playCoin;
    [SerializeField] private GameObject _buyCoin;

    [SerializeField] private GameObject _playHit;
    [SerializeField] private GameObject _buyHit;

    [SerializeField] private GameObject _playWheel;
    [SerializeField] private GameObject _buyWheel;

    private int totalGold;
    [SerializeField] private Text _totalGoldText;


    private void Start()
    {
        if (PlayerPrefs.GetString("ClawGameStatus", "") == "")
        {
            _playClaw.SetActive(false);
            _buyClaw.SetActive(true);
        }

        if (PlayerPrefs.GetString("CoinGameStatus", "") == "")
        {
            _playCoin.SetActive(false);
            _buyCoin.SetActive(true);
        }

        if (PlayerPrefs.GetString("HitGameStatus", "") == "")
        {
            _playHit.SetActive(false);
            _buyHit.SetActive(true);
        }

        if (PlayerPrefs.GetString("WheelGameStatus", "") == "")
        {
            _playWheel.SetActive(false);
            _buyWheel.SetActive(true);
        }

        totalGold = PlayerPrefs.GetInt("totalGold", 0);
    }

    public void BuyClaw()
    {
        if (totalGold >= 100)
        {
            totalGold -= 100;
            PlayerPrefs.SetInt("totalGold", totalGold);
            _totalGoldText.text = totalGold.ToString();

            _buyClaw.SetActive(false);
            _playClaw.SetActive(true);
            PlayerPrefs.SetString("ClawGameStatus", "purchased");
        }
    }

    public void BuyCoin()
    {
        if (totalGold >= 500)
        {
            totalGold -= 500;
            PlayerPrefs.SetInt("totalGold", totalGold);
            _totalGoldText.text = totalGold.ToString();

            _buyCoin.SetActive(false);
            _playCoin.SetActive(true);
            PlayerPrefs.SetString("CoinGameStatus", "purchased");
        } 
    }

    public void BuyHit()
    {
        if (totalGold >= 1000)
        {
            totalGold -= 1000;
            PlayerPrefs.SetInt("totalGold", totalGold);
            _totalGoldText.text = totalGold.ToString();

            _buyHit.SetActive(false);
            _playHit.SetActive(true);
            PlayerPrefs.SetString("HitGameStatus", "purchased");
        }
    }

    public void BuyWheel()
    {
        if (totalGold >= 5000)
        {
            totalGold -= 5000;
            PlayerPrefs.SetInt("totalGold", totalGold);
            _totalGoldText.text = totalGold.ToString();

            _buyWheel.SetActive(false);
            _playWheel.SetActive(true);
            PlayerPrefs.SetString("WheelGameStatus", "purchased");
        }
    }
}