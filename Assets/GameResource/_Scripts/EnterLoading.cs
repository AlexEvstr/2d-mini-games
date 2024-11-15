using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterLoading : MonoBehaviour
{
    [SerializeField] private Image _loadBarWhite;
    private float _loadingTime = 2.5f;
    private float currentTime = 0f;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        StartCoroutine("LoadBar");
    }

    IEnumerator LoadBar()
    {
        while (currentTime < _loadingTime)
        {
            currentTime += Time.deltaTime;
            _loadBarWhite.fillAmount = Mathf.Lerp(0, 1, currentTime / _loadingTime);
            yield return null;
        }

        SceneManager.LoadScene("MainMenu");
    }
}