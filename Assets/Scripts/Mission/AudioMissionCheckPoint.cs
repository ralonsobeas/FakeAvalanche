using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMissionCheckPoint : MonoBehaviour
{
    [SerializeField]
    private AudioClip clipMission;

    [SerializeField] private float sphereRadius;
    public void Start()
    {
        GetComponent<SphereCollider>().radius = sphereRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TerrainGravity>())
        {
            ((GameManager)GameManager.Instance).PlayMissionClip(clipMission);
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
