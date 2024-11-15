using UnityEngine;
using UnityEngine.Advertisements;
using Unity.Advertisement.IosSupport;
using UnityEngine.iOS;

public class AdvertsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private bool _testMode = false;

    private readonly string _androidGameID = "5730344";
    private readonly string _iphoneGameID = "5730345";

    private string _gameID = null;

    public void Start()
    {
        CheckTrackingPermissionAndInitializeAds();
    }

    private void CheckTrackingPermissionAndInitializeAds()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            var trackingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (trackingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();
                Invoke("CheckAndInitializeAdsAfterRequest", 1.0f);
            }
            else
            {
                InitializeAds(trackingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED);
            }
        }
        else
        {
            InitializeAds(true);
        }
    }

    private void CheckAndInitializeAdsAfterRequest()
    {
        var trackingStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        if (trackingStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
        {
            InitializeAds(true);
            GetIDForAds();
        }
        else
        {
            InitializeAds(false);
        }
    }

    private void GetIDForAds()
    {
        ATTrackingStatusBinding.AuthorizationTrackingStatus status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
        {
            string idResult = Device.advertisingIdentifier;
            PlayerPrefs.SetString("idAdverts", idResult);
        }
    }

    public void InitializeAds(bool personalized)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _gameID = _iphoneGameID;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            _gameID = _androidGameID;
        }
        else
        {
            _gameID = _androidGameID;
        }

        Advertisement.Initialize(_gameID, _testMode, this);
    }

    public void OnInitializationComplete()
    {
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {

    }
}