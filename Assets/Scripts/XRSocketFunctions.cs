using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketFunctions : MonoBehaviour
{
    [SerializeField] private GameObject snow;
    private void Awake()
    {
        OnLoadEvent.onLoadedRefugio.AddListener(OnLoadedRefugio);
        OnLoadEvent.onLoadedMission.AddListener(OnLoadedMission);
    }

    private void OnLoadedRefugio() => snow.SetActive(false);
    private void OnLoadedMission() => snow.SetActive(true);

    public void FixAttach(SelectEnterEventArgs args)
    {
        args.interactableObject.transform.SetParent(transform);
    }

    //public void FixUnAttach(SelectExitEventArgs args)
    //{
    //    args.interactableObject.transform.SetParent(null);
    //}
}
