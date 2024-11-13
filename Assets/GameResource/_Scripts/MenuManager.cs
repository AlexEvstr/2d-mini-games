using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform _settingsPanel;
    private Vector2 initialPosition;
    private EnergySystem _energySystem;
    private Coroutine moveCoroutine;

    private AudioMenu _audioMenu;

    private void Start()
    {
        _audioMenu = GetComponent<AudioMenu>();
        initialPosition = _settingsPanel.anchoredPosition;
        _energySystem = GetComponent<EnergySystem>();
    }

    public void OpenSlotMachineGame()
    {
        StartCoroutine(OpenGameScene("SlotMachine"));
    }

    public void OpenClawMachineeGame()
    {
        StartCoroutine(OpenGameScene("ClawMachine"));
    }

    public void OpenCoinDropGame()
    {
        StartCoroutine(OpenGameScene("CoinDrop"));
    }

    public void OpenHitRewardGame()
    {
        StartCoroutine(OpenGameScene("HitReward"));
    }

    public void OpenLuckyWheelGame()
    {
        StartCoroutine(OpenGameScene("LuckyWheel"));
    }

    public void OpenTreasureHuntGame()
    {
        StartCoroutine(OpenGameScene("TreasureHunt"));
    }

    private IEnumerator OpenGameScene(string SceneName)
    {
        if (_energySystem.GetCurrentEnergy() > 0)
        {
            _energySystem.UseEnergy();
            _audioMenu.PlayClickSound();
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneName);
        }
        else
        {
            _audioMenu.PlayDeclineSound();
        }
    }

    public void OpenSettings()
    {
        StartMoving(Vector2.zero);
    }

    public void CloseSettings()
    {   
        StartMoving(initialPosition);
    }

    private void StartMoving(Vector2 destination)
    {
        _audioMenu.PlayClickSound();
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveToPosition(destination));
    }

    private IEnumerator MoveToPosition(Vector2 destination)
    {
        while (Vector2.Distance(_settingsPanel.anchoredPosition, destination) > 10f)
        {
            Vector2 direction = (destination - _settingsPanel.anchoredPosition).normalized;
            _settingsPanel.anchoredPosition += direction * 5000 * Time.deltaTime;

            yield return null;
        }
        _settingsPanel.anchoredPosition = destination;
    }
}