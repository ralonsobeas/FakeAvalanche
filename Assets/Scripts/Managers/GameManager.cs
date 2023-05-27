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
    
    // victim events
    public event OnStart OnVictimIsRescued;
    
    // flag events
    public event OnStart OnPlantFlag;
    
    // alert events
    public event OnAlert OnFireAlert;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        if (OnGameStart != null)
            OnGameStart();
    }

    public void StartMissionTime(int missionSecs)
    {
        if (OnMissionTimeStart != null)
            OnMissionTimeStart(missionSecs);
    }

    public void DecreaseLife()
    {
        if (OnDecreaseLife != null)
            OnDecreaseLife();
    }

    public void Die()
    {
        if (OnDie != null)
            OnDie();
    }

    public void CleanGlasses()
    {
        if (OnCleanGlasses != null)
            OnCleanGlasses();
    }

    public void PlayMissionClip(AudioClip clip, Boolean alert)
    {
        if (OnPlayMissionClip != null)
            OnPlayMissionClip(clip, alert);
    }

    public void EnterInAmbient(AudioSource ambientSource)
    {
        if (OnEnterInAmbient != null)
            OnEnterInAmbient(ambientSource);
    }

    public void LeaveAmbient(AudioSource ambientSource)
    {
        if (OnLeaveAmbient != null)
            OnLeaveAmbient(ambientSource);
    }

    public void FireAlert(Alert alert)
    {
        if (OnFireAlert != null)
            OnFireAlert(alert);
    }

    public void WinMission(int secs)
    {
        if (OnWinMission != null)
            OnWinMission(secs);
    }

    public void LoseMission()
    {
        if (OnLoseMission != null)
            OnLoseMission();
    }

    public void VictimIsDied()
    {
        if (OnVictimIsDied != null)
            OnVictimIsDied();
    }
    
    public void VictimIsRescued()
    {
        if (OnVictimIsRescued != null)
            OnVictimIsRescued();
    }

    public void PlantFlag()
    {
        if (OnPlantFlag != null)
            OnPlantFlag();
    }

}