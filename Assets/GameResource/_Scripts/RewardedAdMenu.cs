using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class RewardedAdMenu : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button _showAdButton;
    [SerializeField] private GameObject _winPopup;
    public Text totalGoldText;
    private int totalGold;

    #region Android ID
    private readonly string _androidAdsID = "Rewarded_Android";
    #endregion

    #region iOS ID
    private readonly string _iOSAdsID = "Rewarded_iOS";
    #endregion

    private string _adId;
    private bool _isAdLoaded = false;

    private void OnEnable()
    {
        _showAdButton.onClick.AddListener(ShowAd);
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _adId = _iOSAdsID;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            _adId = _androidAdsID;
        }
        else
        {
            _adId = _androidAdsID;
        }

        if (!_isAdLoaded)
        {
            _showAdButton.gameObject.SetActive(false);
            LoadAd();
        }
    }

    public void LoadAd()
    {
        Advertisement.Load(_adId, this);
    }

    public void ShowAd()
    {
        _showAdButton.gameObject.SetActive(false);
        Advertisement.Show(_adId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId.Equals(_adId))
        {
            _isAdLoaded = true;
            _showAdButton.gameObject.SetActive(true);
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        _isAdLoaded = false;
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        _isAdLoaded = false;
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            LoadAd();
            OpenPopUpWithCoins();
            _showAdButton.gameObject.SetActive(false);
        }
    }

    public void OpenPopUpWithCoins()
    {
        _winPopup.SetActive(true);
    }

    public void IncreaseGoldSmoothly()
    {
        StartCoroutine(IncrementGoldSmoothly(100));
    }

    private IEnumerator IncrementGoldSmoothly(int amount)
    {
        totalGold = PlayerPrefs.GetInt("totalGold", 0);
        int targetGold = totalGold + amount;
        float currentGold = totalGold;
        float duration = 1f;
        float elapsedTime = 0f;
        _winPopup.SetActive(false);
        while (elapsedTime < duration)
        {
            currentGold = Mathf.Lerp(totalGold, targetGold, elapsedTime / duration);
            totalGoldText.text = Mathf.FloorToInt(currentGold).ToString();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        totalGold = targetGold;
        totalGoldText.text = totalGold.ToString();
        PlayerPrefs.SetInt("totalGold", totalGold);
        
    }
}