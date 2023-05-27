using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Colocar (solo) un GameObject de este tipo en cada escena para que llame al evento onLoaded automáticamente
/// Llamar al evento onLoad cuando se vaya a cargar una nueva escena para avisar al que lo necesite saber.
/// </summary>
public class OnLoadEvent : MonoBehaviour
{
    public static UnityEvent onLoad = new UnityEvent();
    public static UnityEvent onLoaded = new UnityEvent();
    public static UnityEvent onLoadedMission = new UnityEvent();
    public static UnityEvent onLoadedRefugio = new UnityEvent();

    public static bool isMission;

    /// <summary>
    /// Si es false, las llamadas a onLoaded se ignoran.
    /// Si es true, las llamadas a onLoad se ignoran.
    /// </summary>
    private static bool isLoading = true;

    private void Start()
    {
        isMission = FindObjectOfType<TerrainSnowManager>() != null;
        if (isLoading)
        {
            isLoading = false;
            onLoaded.Invoke();
            if (isMission)
                onLoadedMission.Invoke();
        }
        if (!isMission)
            onLoadedRefugio.Invoke();
    }

    public static void OnLoad()
    {
        if (!isLoading)
        {
            isLoading = true;
            onLoad.Invoke();
        }
    }
}
