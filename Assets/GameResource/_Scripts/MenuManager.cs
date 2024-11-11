using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;

    private void Start()
    {
        _coinsText.text = PlayerPrefs.GetInt("TotalCoinsAmount", 0).ToString();
    }

    public void OpenSlotMachineGame()
    {
        SceneManager.LoadScene("SlotMachine");
    }

    public void OpenClawMachineeGame()
    {
        SceneManager.LoadScene("ClawMachine");
    }

    public void OpenCoinDropGame()
    {
        SceneManager.LoadScene("CoinDrop");
    }

    public void OpenHitRewardGame()
    {
        SceneManager.LoadScene("HitReward");
    }

    public void OpenLuckyWheelGame()
    {
        SceneManager.LoadScene("LuckyWheel");
    }

    public void OpenTreasureHuntGame()
    {
        SceneManager.LoadScene("TreasureHunt"); //in development
    }
}