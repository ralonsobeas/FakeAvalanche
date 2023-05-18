using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadThis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnLoadEvent.onLoad.AddListener(OnLoad);
        OnLoadEvent.onLoaded.AddListener(OnLoaded);
    }



    private void OnLoad()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLoaded()
    {
        transform.parent = FindObjectOfType<OnLoadEvent>().transform;
        Invoke("SetPosition",1f);
    }

    private void SetPosition()
    {
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
        transform.parent = null;
    }

    private void OnDestroy()
    {
        OnLoadEvent.onLoad.RemoveListener(OnLoad);
        OnLoadEvent.onLoaded.RemoveListener(OnLoaded);
    }
}
