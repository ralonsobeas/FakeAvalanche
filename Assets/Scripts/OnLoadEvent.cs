using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Colocar (solo) un GameObject de este tipo en cada escena para que llame al evento onLoaded automáticamente
/// Llamar al evento onLoad cuando se vaya a cargar una nueva escena para avisar al que lo necesite saber.
/// </summary>
public class OnLoadEvent : MonoBehaviour
{
    public static UnityEvent onLoad = new UnityEvent();
    public static UnityEvent onLoaded = new UnityEvent();

    /// <summary>
    /// Si es false, las llamadas a onLoaded se ignoran.
    /// Si es true, las llamadas a onLoad se ignoran.
    /// </summary>
    private static bool isLoading = false;

    private void Start()
    {
        if (!isLoading)
            onLoad.Invoke();
    }

    public static void OnLoad()
    {
        if (isLoading)
            onLoaded.Invoke();
    }
}
