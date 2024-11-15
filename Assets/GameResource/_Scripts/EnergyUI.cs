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
    [SerializeField] private TMP_Text _energyTextMenu;

    private EnergySystem energySystem;

    private void Start()
    {
        energySystem = FindObjectOfType<EnergySystem>();

        if (energySystem != null)
        {
            energySystem.onEnergyUpdated += UpdateEnergyUI;

            UpdateEnergyUI(energySystem.GetCurrentEnergy(), energySystem.GetRemainingTime());
            _energyTextMenu.text = energySystem.GetCurrentEnergy().ToString();
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
        }
        else
        {
            fullEnergyObject.SetActive(false);
            timerText.gameObject.SetActive(true);
            
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

            yield return new WaitForSeconds(1);
        }
    }
}