using UnityEngine;

public class AudioGame : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _spinSound;
    [SerializeField] private AudioClip _declineSound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _popSound;

    private int _vibration;
    private VibrationManager _vibrationManager;

    private void Start()
    {
        _vibrationManager = GetComponent<VibrationManager>();
        _musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        _soundsSource.volume = PlayerPrefs.GetFloat("SoundVolume", 1);       
        _vibration = PlayerPrefs.GetInt("Vibration", 1);
    }

    public void PlayClickSound()
    {
        _soundsSource.PlayOneShot(_clickSound);
        if (_vibration == 1) _vibrationManager.TriggerSoftVibration();
    }

    public void PlaySpinSound()
    {
        _soundsSource.PlayOneShot(_spinSound);
        if (_vibration == 1) _vibrationManager.TriggerMediumVibration();
    }

    public void PlayDeclineSound()
    {
        _soundsSource.PlayOneShot(_declineSound);
        if (_vibration == 1) _vibrationManager.TriggerErrorVibration();
    }

    public void PlayWinSound()
    {
        _soundsSource.PlayOneShot(_winSound);
        if (_vibration == 1) _vibrationManager.TriggerStrongVibration();
    }

    public void PlayPopSound()
    {
        _soundsSource.PlayOneShot(_popSound);
        if (_vibration == 1) _vibrationManager.TriggerSoftVibration();
    }
}