using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TerrainGravity>())
        {
            ((GameManager)GameManager.Instance).EnterInAmbient(GetComponent<AudioSource>());
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TerrainGravity>())
        {
            ((GameManager)GameManager.Instance).LeaveAmbient(GetComponent<AudioSource>());
        }
    }
}
