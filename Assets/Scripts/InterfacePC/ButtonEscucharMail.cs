using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEscucharMail : MonoBehaviour
{
    public AudioClip messageClip;
    public AudioSource messageSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (messageSound.isPlaying)
        {
            changeBgColor(Color.green);

        }
        else
        {
            changeBgColor(Color.black);

        }
    }
    private void OnTriggerEnter(Collider other)
    {   // Layer herramientas
        if (other.gameObject.layer == 7)
        {
            changeBgColor(Color.green);

            messageSound.PlayOneShot(messageClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Layer avatar
        if (other.gameObject.layer == 7)
        {
        
            changeBgColor(Color.black);


        }
    }

    /*
     * Cambio de color de fondo del botón
     */
    private void changeBgColor(Color color)
    {
        Debug.Log("BOTON");
        //var colors = GetComponent<Image>().color;
        //colors.normalColor = color;
        GetComponent<Image>().color = color;
    }



}
