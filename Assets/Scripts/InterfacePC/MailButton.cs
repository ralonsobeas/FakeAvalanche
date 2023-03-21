using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailButton : MonoBehaviour
{
    public GameObject pagesContainer;
    private void OnTriggerEnter(Collider other)
    {
        //Esconder todos los child
        foreach (Transform child in pagesContainer.transform)
        {
            child.gameObject.SetActive(false);
        }
        //Activar el hijo de los correos
        pagesContainer.transform.GetChild(1).gameObject.SetActive(true);
    }
}
