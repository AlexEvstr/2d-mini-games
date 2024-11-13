using UnityEngine;
using System;

public class EnergySystem : MonoBehaviour
{
    public int maxEnergy = 9;
    public int energyCooldownMinutes = 1;
    public Action<int, float> onEnergyUpdated;  // Событие для обновления UI

    private int currentEnergy;
    private DateTime lastEnergyTime;
    private float timer;

    private void Start()
    {
        LoadEnergyState();
        InitializeEnergy();
    }

    private void Update()
    {
        UpdateEnergyTimer();
    }

    private void InitializeEnergy()
    {
        if (lastEnergyTime != DateTime.MinValue)
        {
            TimeSpan timePassed = DateTime.Now - lastEnergyTime;
            int energyToAdd = (int)(timePassed.TotalMinutes / energyCooldownMinutes);
            currentEnergy = Mathf.Min(currentEnergy + energyToAdd, maxEnergy);
            float timerSaved = PlayerPrefs.GetFloat("Timer", energyCooldownMinutes * 60);
            float timerdiff = 60 - timerSaved;
            timer = Mathf.Clamp(energyCooldownMinutes * 60 - (float)timePassed.TotalSeconds % (energyCooldownMinutes * 60), 0, energyCooldownMinutes * 60);
            timer -= timerdiff;
            if (timer < 0) timer = 60;
            Debug.Log("Timer-2: " + timer);
            SaveEnergyState();
        }

        onEnergyUpdated?.Invoke(currentEnergy, timer); // Обновляем UI при инициализации
    }

    private void UpdateEnergyTimer()
    {
        if (currentEnergy < maxEnergy)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                currentEnergy++;
                SaveEnergyState();

                if (currentEnergy < maxEnergy)
                {
                    timer = energyCooldownMinutes * 60;
                }

                onEnergyUpdated?.Invoke(currentEnergy, timer);
            }
        }
    }

    public void UseEnergy()
    {
        if (currentEnergy > 0)
        {
            currentEnergy--;
            

            // Устанавливаем таймер только если он не активен
            if (currentEnergy == maxEnergy)
            {
                timer = energyCooldownMinutes * 60;
            }

            onEnergyUpdated?.Invoke(currentEnergy, timer);
            SaveEnergyState();
        }
    }


    // Публичный метод для получения текущей энергии
    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }

    // Публичный метод для получения оставшегося времени
    public float GetRemainingTime()
    {
        return timer;
    }

    private void SaveEnergyState()
    {
        PlayerPrefs.SetInt("CurrentEnergy", currentEnergy);
        PlayerPrefs.SetString("LastEnergyTime", DateTime.Now.ToString("o"));
        PlayerPrefs.SetFloat("Timer", timer);
        PlayerPrefs.Save();
    }

    private void LoadEnergyState()
    {
        currentEnergy = PlayerPrefs.GetInt("CurrentEnergy", maxEnergy);
        string lastEnergyTimeStr = PlayerPrefs.GetString("LastEnergyTime", DateTime.MinValue.ToString("o"));
        DateTime.TryParse(lastEnergyTimeStr, out lastEnergyTime);
        timer = PlayerPrefs.GetFloat("Timer", energyCooldownMinutes * 60);
        Debug.Log(timer);
    }

    private void OnApplicationQuit()
    {
        SaveEnergyState();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveEnergyState();
    }
}
