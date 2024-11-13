using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class EnergyUI : MonoBehaviour
{
    public Image energyImage;
    public GameObject fullEnergyObject;
    public TMP_Text timerText;
    public Sprite[] energySprites;

    private EnergySystem energySystem;

    private void Start()
    {
        energySystem = FindObjectOfType<EnergySystem>();

        if (energySystem != null)
        {
            // Подписываемся на событие обновления энергии
            energySystem.onEnergyUpdated += UpdateEnergyUI;

            // Инициализируем UI сразу после загрузки состояния
            UpdateEnergyUI(energySystem.GetCurrentEnergy(), energySystem.GetRemainingTime());

            // Останавливаем предыдущую корутину (если была), чтобы избежать сброса
            StopCoroutine(UpdateTimerCoroutine());
            StartCoroutine(UpdateTimerCoroutine());
        }
    }


    private void UpdateEnergyUI(int currentEnergy, float remainingTime)
    {
        if (currentEnergy == energySprites.Length - 1)
        {
            fullEnergyObject.SetActive(true);
            timerText.gameObject.SetActive(false);
            //energyImage.gameObject.SetActive(false);
        }
        else
        {
            fullEnergyObject.SetActive(false);
            //energyImage.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);
            

            // Обновляем UI таймера сразу после загрузки
            TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
            timerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
        energyImage.sprite = energySprites[currentEnergy];
    }

    private IEnumerator UpdateTimerCoroutine()
    {
        while (true)
        {
            if (energySystem != null && energySystem.GetCurrentEnergy() < energySprites.Length)
            {
                float remainingTime = energySystem.GetRemainingTime();
                TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
                timerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            }

            yield return new WaitForSeconds(1); // Обновляем таймер каждую секунду
        }
    }
}
