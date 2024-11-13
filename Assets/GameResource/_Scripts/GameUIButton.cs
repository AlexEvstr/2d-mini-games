using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIButton : MonoBehaviour
{
    [SerializeField] private Button _menuBtn;
    private AudioGame _audioGame;

    private void Start()
    {
        _audioGame = GetComponent<AudioGame>();
        _menuBtn.onClick.AddListener(BackToMenuButton);
    }

    private void BackToMenuButton()
    {
        StartCoroutine(WaitAndGoToMenu());
    }

    private IEnumerator WaitAndGoToMenu()
    {
        _audioGame.PlayClickSound();
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("MainMenu");
    }
}