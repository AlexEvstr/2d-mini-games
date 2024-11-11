using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGamesSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] _games;
    private int _currentGame;

    private void Start()
    {
        _currentGame = PlayerPrefs.GetInt("CurrentGameIndex", 0);
        _games[_currentGame].SetActive(true);
    }

    public void ShowNextGame()
    {
        _games[_currentGame].SetActive(false);
        _currentGame++;
        if (_currentGame == _games.Length) _currentGame = 0;
        _games[_currentGame].SetActive(true);
        PlayerPrefs.SetInt("CurrentGameIndex", _currentGame);
    }

    public void ShowPreviousGame()
    {
        _games[_currentGame].SetActive(false);
        _currentGame--;
        if (_currentGame < 0) _currentGame = _games.Length-1;
        _games[_currentGame].SetActive(true);
        PlayerPrefs.SetInt("CurrentGameIndex", _currentGame);
    }
}
