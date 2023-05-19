using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicGame, sfxHurt, sfxDie, sfxCleanGlasses;
    
    
    public AudioSource mainSource, leftSource;
    private LinkedList<AudioClip> queueClips;
    private Boolean isPreviousClipInQueue, isAudioAlert;
    private float currentTimeAudioClip;
    
    void Start()
    {
        ((GameManager)GameManager.Instance).OnDecreaseLife += StartSfxHurt;
        ((GameManager)GameManager.Instance).OnCleanGlasses += StartSfxCleanGlasses;
        ((GameManager)GameManager.Instance).OnDie += StartSfxDie;
        ((GameManager)GameManager.Instance).OnPlayMissionClip += AddClipToQueue;

        queueClips = new LinkedList<AudioClip>();
    }

    public void StartSfxHurt()
    {
        mainSource.PlayOneShot(sfxHurt,0.4f);
    }

    public void AddClipToQueue(AudioClip clip, Boolean isAlert)
    {
        isAudioAlert = isAlert;
        if (isAudioAlert)
        {
            if (leftSource.isPlaying)
            {
                currentTimeAudioClip = leftSource.time;
            }
            queueClips.AddFirst(clip);
        }
        else
        {
            queueClips.AddLast(clip);    
        }
        
    }

    public void StartSfxDie()
    {
        mainSource.PlayOneShot(sfxDie,0.4f);
    }

    public void StartSfxCleanGlasses()
    {
        mainSource.PlayOneShot(sfxCleanGlasses, 0.4f);
    }

    private void PlayNextClipFromQueue()
    {
        if (queueClips.Count == 0)
        {
            return;
        }
        isPreviousClipInQueue = true;
        leftSource.clip = queueClips.ElementAt(0);
        if (isAudioAlert)
        {
            leftSource.Play();
        }
        else
        {
            leftSource.time = currentTimeAudioClip;
            leftSource.PlayDelayed(0.2f);
            currentTimeAudioClip = 0;
        }
       
    }
    private void Update()
    {
        if (isAudioAlert && queueClips.Count > 0 )
        {
            PlayNextClipFromQueue();
            isAudioAlert = false;
        }
        if (queueClips.Count > 0 && !leftSource.isPlaying)
        {
            if (isPreviousClipInQueue)
            {
                isPreviousClipInQueue = false;
                queueClips.RemoveFirst();
            }
            PlayNextClipFromQueue();
        }
    }
}