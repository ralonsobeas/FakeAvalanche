using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailButton : MonoBehaviour
{
    public GameObject pagesContainer;
    public GameObject mailContainer;
    public GameObject activeMailContainer;
    public InterfaceManager interfaceManager;

    private void OnTriggerEnter(Collider other)
    {
        //StartCoroutine(MostrarBotonAudio(2));
        interfaceManager.mostrarBotonDelay(activeMailContainer.transform.Find("ButtonEscucharMensaje").gameObject);
        //Esconder todos los child
        foreach (Transform child in pagesContainer.transform)
        {
            child.gameObject.SetActive(false);
        }
        //Activar el hijo de los correos
        mailContainer.SetActive(true);

    }



    IEnumerator MostrarBotonAudio(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        activeMailContainer.transform.Find("ButtonEscucharMensaje").gameObject.SetActive(true);

    }
}


