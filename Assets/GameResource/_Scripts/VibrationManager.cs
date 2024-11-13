using System.Runtime.InteropServices;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ActivateSoftVibration();

    [DllImport("__Internal")]
    private static extern void ActivateMediumVibration();

    [DllImport("__Internal")]
    private static extern void ActivateStrongVibration();

    [DllImport("__Internal")]
    private static extern void ActivateErrorVibration();

    public void TriggerSoftVibration()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            ActivateSoftVibration();
        }
    }

    public void TriggerMediumVibration()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            ActivateMediumVibration();
        }
    }

    public void TriggerStrongVibration()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            ActivateStrongVibration();
        }
    }

    public void TriggerErrorVibration()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            ActivateErrorVibration();
        }
    }
}
