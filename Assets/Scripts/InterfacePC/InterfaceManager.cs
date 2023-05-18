using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public bool canPress;
    // Start is called before the first frame update
    void Start()
    {
        canPress=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarBotonDelay(GameObject boton)
    {
        StartCoroutine(MostrarBotonAudio(2, boton));
    }


    IEnumerator MostrarBotonAudio(float time, GameObject boton)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        boton.gameObject.SetActive(true);

    }
}
