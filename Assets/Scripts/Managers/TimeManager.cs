using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int firstAlertToDie = 100;
    [SerializeField] private int secondAlertToDie = 40;
    [SerializeField] private int missionSecs = 200;
    private float time;
    private bool alertIsSend;

    private void Awake()
    {
        ((GameManager)GameManager.Instance).OnVictimIsRescued += VictimIsRescued;
    }

    void Start()
    {
        MissionStart();
    }

    public void VictimIsRescued()
    {
        ((GameManager)GameManager.Instance).WinMission((int)time);
    }

    public void MissionStart()
    {
        ((GameManager)GameManager.Instance).StartMissionTime(missionSecs);
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

    private void Update()
    {
        time += Time.deltaTime;
        if (time > missionSecs && !alertIsSend)
        {
            ((GameManager)GameManager.Instance).FireAlert(Alert.VictimProbablyIsDied);
            ((GameManager)GameManager.Instance).VictimIsDied();
            alertIsSend = true;
        }
    }
}
