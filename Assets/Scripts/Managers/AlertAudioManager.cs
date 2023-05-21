using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class AlertAudioManager : MonoBehaviour
{
    public AudioClip alertDecreaseLife,
        alertStayAfk,
        alertFogGlasses,
        alertTooFarFromObjective,
        alertFiveMinsToDie,
        alertTwoMinsToDie; 
    void Start()
    {
        ((GameManager)GameManager.Instance).OnFireAlert += FireAlert;
    }

    public void FireAlert(Alert alert)
    {
        switch (alert)
        {
            case Alert.DecreaseLife:
                ((GameManager)GameManager.Instance).PlayMissionClip(alertDecreaseLife, true);
                break;
            case Alert.StayAfk:
                ((GameManager)GameManager.Instance).PlayMissionClip(alertStayAfk, true);
                break;
            case Alert.FogGlasses:
                ((GameManager)GameManager.Instance).PlayMissionClip(alertFogGlasses, true);
                break;
            case Alert.TooFarFromObjective:
                ((GameManager)GameManager.Instance).PlayMissionClip(alertTooFarFromObjective, true);
                break;
            case Alert.FiveMinsToDie:
                ((GameManager)GameManager.Instance).PlayMissionClip(alertFiveMinsToDie, true);
                break;
            case Alert.TwoMinsToDie:
                ((GameManager)GameManager.Instance).PlayMissionClip(alertTwoMinsToDie, true);
                break;
            default:
                Debug.Log("alert not found-> " + alert);
                break;
        }
    }
}
