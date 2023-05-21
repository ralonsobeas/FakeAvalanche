using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton
{
    public delegate void OnStart();
    public delegate void OnTimeStart(int secs);
    public delegate void OnPlayClip(AudioClip clip, Boolean alert);

    public delegate void OnAmbientTrigger(AudioSource ambient);
    
    public delegate void OnAlert(Alert alert);

    public event OnStart OnGameStart;
    
    // health events
    public event OnStart OnDecreaseLife;
    public event OnStart OnDie;
    public event OnStart OnCleanGlasses;
    
    // audio events
    public event OnPlayClip OnPlayMissionClip;
    public event OnAmbientTrigger OnEnterInAmbient;
    public event OnAmbientTrigger OnLeaveAmbient;
    
    // time events
    public event OnTimeStart OnMissionTimeStart;
    public event OnTimeStart OnWinMission;
    public event OnStart OnLoseMission;

    public event OnStart OnVictimIsDied;
    // alert events
    public event OnAlert OnFireAlert;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        OnGameStart();
    }

    public void StartMissionTime(int missionSecs)
    {
        OnMissionTimeStart(missionSecs);
    }

    public void DecreaseLife()
    {
        OnDecreaseLife();
    }

    public void Die()
    {
        OnDie();
    }

    public void CleanGlasses()
    {
        OnCleanGlasses();
    }

    public void PlayMissionClip(AudioClip clip, Boolean alert)
    {
        OnPlayMissionClip(clip, alert);
    }

    public void EnterInAmbient(AudioSource ambientSource)
    {
        OnEnterInAmbient(ambientSource);
    }

    public void LeaveAmbient(AudioSource ambientSource)
    {
        OnLeaveAmbient(ambientSource);
    }

    public void FireAlert(Alert alert)
    {
        OnFireAlert(alert);
    }

    public void WinMission(int secs)
    {
        OnWinMission(secs);
    }

    public void LoseMission()
    {
        OnLoseMission();
    }

    public void VictimIsDied()
    {
        OnVictimIsDied();
    }

}