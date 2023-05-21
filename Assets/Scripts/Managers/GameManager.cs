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
    public event OnStart OnDecreaseLife;
    public event OnStart OnDie;
    public event OnStart OnCleanGlasses;
    public event OnPlayClip OnPlayMissionClip;

    public event OnAmbientTrigger OnEnterInAmbient;
    public event OnAmbientTrigger OnLeaveAmbient;
    public event OnTimeStart OnMissionTimeStart;

    public event OnAlert OnFireAlert;

    [Range(15, 1200)] public int missionSecs = 15;

    private void Start()
    {
        StartGame();
        StartMissionTime();
    }

    public void StartGame()
    {
        OnGameStart();
    }

    public void StartMissionTime()
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

}