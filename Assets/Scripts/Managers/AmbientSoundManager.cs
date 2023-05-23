using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundManager : MonoBehaviour
{
    public AudioSource windSource;

    private void Awake()
    {
        ((GameManager)GameManager.Instance).OnGameStart += StartWindAmbient;
        ((GameManager)GameManager.Instance).OnEnterInAmbient += EnterInAmbient;
        ((GameManager)GameManager.Instance).OnLeaveAmbient += LeaveAmbient;
    }

    void Start()
    {
        StartAmbientSource();
    }

    void StartAmbientSource()
    {
        windSource.loop = true;
        windSource.volume = 1f;
    }

    public void StartWindAmbient()
    {
        Debug.Log("arranco viento");
        windSource.Play();
    }

    public void EnterInAmbient(AudioSource ambientSource)
    {
        windSource.volume = 0.001f;
        ambientSource.loop = true;
        ambientSource.volume = 1;
        ambientSource.Play();
    }

    public void LeaveAmbient(AudioSource ambientSource)
    {
        windSource.volume = 1f;
        ambientSource.Stop();
    }
}
