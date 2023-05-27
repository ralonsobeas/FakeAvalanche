using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    public string loadingSceneName;
    public string levelSceneName;
    public float timeToTeleport = 0;

    private bool hasContacted = false;
    private float time = 0;

    private void Update()
    {
        if (hasContacted)
        {
            time += Time.deltaTime;
            if (time >= timeToTeleport)
            {
                StartCoroutine(LoadYourAsyncScene());
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        //TODO: cambiar la condición de reconocimiento de pala
        if(collision.gameObject.name == "Pala")
        {
            hasContacted = true;
        }
    }
    IEnumerator LoadYourAsyncScene()
    {
        LoadingLevelName.NextLevelName = levelSceneName;
        OnLoadEvent.OnLoad();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadingSceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
