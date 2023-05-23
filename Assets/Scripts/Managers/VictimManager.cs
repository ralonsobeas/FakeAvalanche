using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimManager : MonoBehaviour
{
    public AudioClip victimWithLifeRescued, victimDiedRescued;

    public bool isDied;

    private void Awake()
    {
        ((GameManager)GameManager.Instance).OnVictimIsDied += VictimIsDied;
    }

    void VictimIsDied()
    {
        isDied = true;
    }

    public void VictimIsRescued()
    {
        if (isDied)
        {
            ((GameManager)GameManager.Instance).PlayMissionClip(victimWithLifeRescued, false);
        }
        else
        {
            ((GameManager)GameManager.Instance).PlayMissionClip(victimDiedRescued, false);
        }
    }
}
