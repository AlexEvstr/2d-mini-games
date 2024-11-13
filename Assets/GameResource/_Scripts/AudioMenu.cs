using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _buySound;
    [SerializeField] private AudioClip _declineSound;

    [SerializeField] private GameObject _musicOn;
    [SerializeField] private GameObject _musicOff;
    [SerializeField] private GameObject _soundOn;
    [SerializeField] private GameObject _soundOff;
    [SerializeField] private GameObject _vibroOn;
    [SerializeField] private GameObject _vibroOff;

    private int _vibration;
    private VibrationManager _vibrationManager;

    private void Start()
    {
        _vibrationManager = GetComponent<VibrationManager>();

        _musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        if (_musicSource.volume == 1)
        {
            _musicOff.SetActive(false);
            _musicOn.SetActive(true);
        }
        else
        {
            _musicOn.SetActive(false);
            _musicOff.SetActive(true);
        }

        _soundsSource.volume = PlayerPrefs.GetFloat("SoundVolume", 1);
        if (_soundsSource.volume == 1)
        {
            _soundOff.SetActive(false);
            _soundOn.SetActive(true);
        }
        else
        {
            _soundOn.SetActive(false);
            _soundOff.SetActive(true);
        }

        _vibration = PlayerPrefs.GetInt("Vibration", 1);
        if (_vibration == 1)
        {
            _vibroOff.SetActive(false);
            _vibroOn.SetActive(true);
        }
        else
        {
            _vibroOn.SetActive(false);
            _vibroOff.SetActive(true);
        }
    }

    public void OffMusic()
    {
        _musicOn.SetActive(false);
        _musicOff.SetActive(true);
        _musicSource.volume = 0;
        PlayerPrefs.SetFloat("MusicVolume", _musicSource.volume);
        PlayClickSound();
    }

    public void OnMusic()
    {
        _musicOff.SetActive(false);
        _musicOn.SetActive(true);
        _musicSource.volume = 1;
        PlayerPrefs.SetFloat("MusicVolume", _musicSource.volume);
        PlayClickSound();
    }

    public void OffSound()
    {
        _soundOn.SetActive(false);
        _soundOff.SetActive(true);
        _soundsSource.volume = 0;
        PlayerPrefs.SetFloat("SoundVolume", _soundsSource.volume);
        PlayClickSound();
    }

    public void OnSound()
    {
        _soundOff.SetActive(false);
        _soundOn.SetActive(true);
        _soundsSource.volume = 1;
        PlayerPrefs.SetFloat("SoundVolume", _soundsSource.volume);
        PlayClickSound();
    }

    public void OffVibro()
    {
        _vibroOn.SetActive(false);
        _vibroOff.SetActive(true);
        _vibration = 0;
        PlayerPrefs.SetInt("Vibration", _vibration);
        PlayClickSound();
    }

    public void OnVibro()
    {
        _vibroOff.SetActive(false);
        _vibroOn.SetActive(true);
        _vibration = 1;
        PlayerPrefs.SetInt("Vibration", _vibration);
        PlayClickSound();
    }

    public void PlayClickSound()
    {
        _soundsSource.PlayOneShot(_clickSound);
        _vibrationManager.TriggerSoftVibration();
    }

    public void PlayBuySound()
    {
        _soundsSource.PlayOneShot(_buySound);
        _vibrationManager.TriggerStrongVibration();
    }

    public void PlayDeclineSound()
    {
        _soundsSource.PlayOneShot(_declineSound);
        _vibrationManager.TriggerErrorVibration();
    }
}
