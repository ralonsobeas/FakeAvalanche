using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicGame, sfxHurt, sfxDie, sfxCleanGlasses;
    public AudioSource mainSource, leftSource;
    void Start()
    {
        ((GameManager)GameManager.Instance).OnDecreaseLife += StartSfxHurt;
        ((GameManager)GameManager.Instance).OnCleanGlasses += StartSfxCleanGlasses;
        ((GameManager)GameManager.Instance).OnDie += StartSfxDie;
        ((GameManager)GameManager.Instance).OnPlayMissionClip += PlayMissionClip;
    }

    public void StartSfxHurt()
    {
        mainSource.PlayOneShot(sfxHurt,0.4f);
    }

    public void PlayMissionClip(AudioClip clip)
    {
        Debug.Log("entro a reproducir clip");
        leftSource.PlayOneShot(clip);
    }

    public void StartSfxDie()
    {
        mainSource.PlayOneShot(sfxDie,0.4f);
    }

    public void StartSfxCleanGlasses()
    {
        mainSource.PlayOneShot(sfxCleanGlasses, 0.4f);
    }
}