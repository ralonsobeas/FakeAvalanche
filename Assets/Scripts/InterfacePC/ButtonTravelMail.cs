using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonTravelMail : MonoBehaviour
{
    public string sceneName;

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
        if (other.gameObject.layer == 7)
        {
            changeBgColor(Color.green);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Layer avatar
        if (other.gameObject.layer == 7)
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene());
            changeBgColor(Color.red);

        }
    }

    /*
     * Cambio de color de fondo del botón
     */
    private void changeBgColor(Color color)
    {
        var colors = GetComponent<Button>().colors;
        colors.normalColor = color;
        GetComponent<Button>().colors = colors;
    }


    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
