using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton
{
    public delegate void OnStart();
    

    public event OnStart OnGameStart;
    public event OnStart OnDecreaseLife;
    public event OnStart OnDie;
    public event OnStart OnCleanGlasses;

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

    public void PlayNextCheckWalkie()
    {
        OnVoiceWalkieTalkie();
    }
}