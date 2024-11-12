using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIButton : MonoBehaviour
{
    [SerializeField] private Button _menuBtn;

    private void Start()
    {
        _menuBtn.onClick.AddListener(BackToMenuButton);
    }

    private void BackToMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}