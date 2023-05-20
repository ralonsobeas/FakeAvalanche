using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundManager : MonoBehaviour
{
    public AudioSource windSource;
    void Start()
    {
        StartAmbientSource();
        ((GameManager)GameManager.Instance).OnGameStart += StartWindAmbient;
        ((GameManager)GameManager.Instance).OnEnterInAmbient += EnterInAmbient;
        ((GameManager)GameManager.Instance).OnLeaveAmbient += LeaveAmbient;
       
    }

    void StartAmbientSource()
    {
        windSource.loop = true;
        windSource.volume = 1f;
    }

    public void StartWindAmbient()
    {
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
