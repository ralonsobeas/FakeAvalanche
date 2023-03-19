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
}