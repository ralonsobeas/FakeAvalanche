using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagRemove : MonoBehaviour
{
    [SerializeField]
    private GameObject flagCloth;

    private bool desplegada = false;

    public void plegarDesplegar()
    {
        desplegada = !desplegada;
        flagCloth.SetActive(desplegada);
    }
}
