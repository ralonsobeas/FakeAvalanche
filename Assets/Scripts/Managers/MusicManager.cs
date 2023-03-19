using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip musicGame, sfxHurt, sfxDie, sfxCleanGlasses;
    public AudioSource mainSource, secondarySource, leftSource;
    
    void Start()
    {
        ((GameManager)GameManager.Instance).OnGameStart += StartMusicGame;
        ((GameManager)GameManager.Instance).OnDecreaseLife += StartSfxHurt;
        ((GameManager)GameManager.Instance).OnCleanGlasses += StartSfxCleanGlasses;
        ((GameManager)GameManager.Instance).OnDie += StartSfxDie;
    }

    public void StartMusicGame()
    {
        mainSource.clip = musicGame;
        mainSource.loop = true;
        mainSource.volume = 0.1f;
        StartMusic();
    }

    public void StartSfxHurt()
    {
        secondarySource.PlayOneShot(sfxHurt,0.4f);
    }

    public void StartSfxDie()
    {
        secondarySource.PlayOneShot(sfxDie,0.4f);
    }

    public void StartSfxCleanGlasses()
    {
        secondarySource.PlayOneShot(sfxCleanGlasses, 0.4f);
    }

    public void StartMusic()
    {
        mainSource.Play();
    }

    public void StopMusic()
    {
        mainSource.Stop();
    }
}