using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadThis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnLoadEvent.onLoad.AddListener(OnLoad);
    }



    private void OnLoad()
    {
        transform.parent = FindObjectOfType<OnLoadEvent>().transform;
        transform.parent = null;
    }

    private void OnDestroy()
    {
        OnLoadEvent.onLoad.RemoveListener(OnLoad);
    }
}
