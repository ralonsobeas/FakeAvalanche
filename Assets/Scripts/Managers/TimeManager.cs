using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int firstAlertToDie = 100;
    [SerializeField] private int secondAlertToDie = 40;

    private int time;
    void Start()
    {
        ((GameManager)GameManager.Instance).OnMissionTimeStart += MissionStart;
    }

    public void MissionStart(int missionSecs)
    {
        int timeToFirstAlert = missionSecs - firstAlertToDie;
        int timeToSecondAlert = missionSecs - secondAlertToDie;
        Invoke("CountdownFirstAlertTime", timeToFirstAlert);
        Invoke("CountdownSecondAlertTime", timeToSecondAlert);
    }

    public void CountdownFirstAlertTime()
    {
        Debug.Log("primera alerta");
        ((GameManager)GameManager.Instance).FireAlert(Alert.FirstAlertToDie);
    }
    
    public void CountdownSecondAlertTime()
    {
        Debug.Log("segunda alerta");
        ((GameManager)GameManager.Instance).FireAlert(Alert.SecondAlertToDie);
    }
}
