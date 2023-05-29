using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCorreo : MonoBehaviour
{

    public InterfaceManager interfaceManager;

    public GameObject pagesContainer;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Index")) return;
        //Esconder todos los child
        foreach (Transform child in pagesContainer.transform)
        {
            child.gameObject.SetActive(false);
        }
        //Activar el hijo de los correos
        pagesContainer.transform.GetChild(0).gameObject.SetActive(true);
    }
}
