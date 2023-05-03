using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicGame, sfxHurt, sfxDie, sfxCleanGlasses;
    public AudioSource mainSource, leftSource;

    private int counterVoicesWalkieTalkie = 0;
    void Start()
    {
        ((GameManager)GameManager.Instance).OnDecreaseLife += StartSfxHurt;
        ((GameManager)GameManager.Instance).OnCleanGlasses += StartSfxCleanGlasses;
        ((GameManager)GameManager.Instance).OnDie += StartSfxDie;
        ((GameManager)GameManager.Instance).OnVoiceWalkieTalkie += StartVoiceWalkie;
    }

    public void StartSfxHurt()
    {
        mainSource.PlayOneShot(sfxHurt,0.4f);
    }

    public void StartVoiceWalkie()
    {
        Debug.Log("entro a reproducir " + counterVoicesWalkieTalkie + " " + ((GameManager)GameManager.Instance).checksVoiceWalkie[counterVoicesWalkieTalkie]);
        if (counterVoicesWalkieTalkie < ((GameManager)GameManager.Instance).checksVoiceWalkie.Length - 1)
        {
            leftSource.PlayOneShot(((GameManager)GameManager.Instance).checksVoiceWalkie[counterVoicesWalkieTalkie]);
            counterVoicesWalkieTalkie++;
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
}