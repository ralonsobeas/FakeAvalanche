using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingAsyncManager : MonoBehaviour
{
    private float time = 0;
    public float timeToStartLoading = 3;

    private bool loadedOnce = false;
    // Update is called once per frame
    public AudioClip helicopterClip;
    public AudioSource soundSource;
    public Vector3 start;
    public float seconds = 9000;
    public Vector3 Point;
    public Vector3 Difference;
    public float timer;
    public float percent;
    private void Start()
    {

        soundSource.PlayOneShot(helicopterClip);
        start = transform.position;

    }
    void Update()
    {
        time += Time.deltaTime;
        if (!loadedOnce && time >= timeToStartLoading)
        {
            loadedOnce = true;
            StartCoroutine(LoadYourAsyncScene());
        }
        Vector3 temp = this.transform.position;
        temp.x += 0.05f;
        transform.position = temp;
    }

    IEnumerator LoadYourAsyncScene()
    {
        OnLoadEvent.OnLoad();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LoadingLevelName.NextLevelName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
