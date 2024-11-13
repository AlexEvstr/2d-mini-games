using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private RectTransform _settingsPanel;
    private Vector2 initialPosition;
    private EnergySystem _energySystem;
    private Coroutine moveCoroutine;

    private void Start()
    {
        initialPosition = _settingsPanel.anchoredPosition;
        _energySystem = GetComponent<EnergySystem>();
        _coinsText.text = PlayerPrefs.GetInt("TotalCoinsAmount", 0).ToString();
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
        _energySystem.UseEnergy();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneName);
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
        // Останавливаем любую текущую корутину, если она запущена
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        // Запускаем новую корутину для перемещения
        moveCoroutine = StartCoroutine(MoveToPosition(destination));
    }

    // Корутина для линейного перемещения
    private IEnumerator MoveToPosition(Vector2 destination)
    {
        while (Vector2.Distance(_settingsPanel.anchoredPosition, destination) > 10f)
        {
            // Вычисляем направление и линейное перемещение
            Vector2 direction = (destination - _settingsPanel.anchoredPosition).normalized;
            _settingsPanel.anchoredPosition += direction * 5000 * Time.deltaTime;

            yield return null; // Ждем следующий кадр
        }

        // Устанавливаем точную позицию в конце, чтобы избежать небольших отклонений
        _settingsPanel.anchoredPosition = destination;
    }
}