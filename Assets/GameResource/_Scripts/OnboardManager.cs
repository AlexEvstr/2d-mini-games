using UnityEngine;

public class OnboardManager : MonoBehaviour
{
    [SerializeField] private GameObject _onboardWindow;
    [SerializeField] private GameObject[] _onboards;
    private string _enterStatus;
    private AudioMenu _audioMenu;

    private void OnEnable()
    {
        _audioMenu = GetComponent<AudioMenu>();
        _enterStatus = PlayerPrefs.GetString("EnterOnboardStatsu", "");
        if (_enterStatus == "")
        {
            _onboardWindow.SetActive(true);
            _onboards[0].SetActive(true);
        }
    }

    public void Open1Onboard()
    {
        _onboardWindow.SetActive(true);
        _onboards[0].SetActive(true);
        _audioMenu.PlayClickSound();
    }

    public void Open2OnBoard()
    {
        _onboards[0].SetActive(false);
        _onboards[1].SetActive(true);
        _audioMenu.PlayClickSound();
    }

    public void Open3OnBoard()
    {
        _onboards[1].SetActive(false);
        _onboards[2].SetActive(true);
        _audioMenu.PlayClickSound();
    }

    public void OpenMenu()
    {
        _onboards[2].SetActive(false);
        PlayerPrefs.SetString("EnterOnboardStatsu", "Shown");
        _audioMenu.PlayClickSound();
    }
}