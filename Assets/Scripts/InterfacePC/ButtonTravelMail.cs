using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonTravelMail : MonoBehaviour
{
    public string loadingSceneName;
    public string levelSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {   // Layer herramientas
        if (!other.CompareTag("Index")) return;
        if (other.gameObject.layer == 7)
        {
            changeBgColor(Color.green);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Index")) return;
        // Layer avatar
        if (other.gameObject.layer == 7)
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene());
            changeBgColor(Color.red);

        }
    }

    /*
     * Cambio de color de fondo del bot�n
     */
    private void changeBgColor(Color color)
    {
        GetComponent<Image>().color = color;
    }

    [ContextMenu("LoadScene")]
    private void LoadScene() => StartCoroutine(LoadYourAsyncScene());

    IEnumerator LoadYourAsyncScene()
    {
        LoadingLevelName.NextLevelName = levelSceneName;
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        LoadingAsyncManager.scene = levelSceneName;
        OnLoadEvent.OnLoad();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadingSceneName);
        
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
