using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketFunctions : MonoBehaviour
{
    public void FixAttach(SelectEnterEventArgs args)
    {
        args.interactableObject.transform.SetParent(transform);
    }

    //public void FixUnAttach(SelectExitEventArgs args)
    //{
    //    args.interactableObject.transform.SetParent(null);
    //}
}
