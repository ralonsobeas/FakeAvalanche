using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton
{
    public delegate void OnStart();
    public delegate void OnPlayClip(AudioClip clip);

    public event OnStart OnGameStart;
    public event OnStart OnDecreaseLife;
    public event OnStart OnDie;
    public event OnStart OnCleanGlasses;
    public event OnPlayClip OnPlayMissionClip; 

    

    public AudioClip[] checksVoiceWalkie;

    public event OnStart OnVoiceWalkieTalkie;

    public void StartGame()
    {
        OnGameStart();
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

    [ContextMenu("PlayNextStepWalkie")]
    public void PlayNextCheckWalkie()
    {
        OnVoiceWalkieTalkie();
    }

    public void PlayMissionClip(AudioClip clip)
    {
        OnPlayMissionClip(clip);
    }

}