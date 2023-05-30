using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class DirectionalAudio : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform audioListenerObject;

    [SerializeField] private Transform transformReference;

    [SerializeField] private Color rayColor = Color.red;

    [SerializeField] private int raycastLength = 20;

    [SerializeField]
    private Boolean isLinkToAnotherReference;

    [SerializeField] private Transform linkTransformReference;

    private void Awake()
    {
        OnLoadEvent.onLoadedMission.AddListener(OnLoadedMission);
    }

    private void OnLoadedMission()
    {
        audioListenerObject = FindObjectOfType<TrackedPoseDriver>().transform;
    }

    private void Start()
    {
        if (isLinkToAnotherReference)
        {
            SetTransformReference(linkTransformReference);
            return;
        }
        
        if (transformReference != null)
        {
            SetTransformReference(transformReference);
            return;
        }
        SetTransformReference(transform);
        
    }

    private void SetTransformReference(Transform paramTransform)
    {
        transformReference = paramTransform;
    }

    // Update is called once per frame
    void Update()
    {
        // Calcula la dirección relativa desde el objeto del audio al objeto del audioListener
        Vector3 relativeDirection = audioListenerObject.position - transformReference.position;

        // Calcula la rotación hacia el objeto del audioSource
        Quaternion sourceRotation = Quaternion.LookRotation(relativeDirection, transformReference.up);

        // Calcula la rotación hacia el objeto del audioListener
        Quaternion listenerRotation = Quaternion.LookRotation(audioListenerObject.transform.forward, audioListenerObject.transform.up);

        // Calcula la dirección frontal relativa del objeto del audioSource
        Vector3 forwardDirectionSource = Quaternion.Inverse(listenerRotation) * sourceRotation * transformReference.forward;

        // Calcula el ángulo de panoramización basado en la dirección frontal relativa
        float angle = Vector3.SignedAngle(Vector3.forward, forwardDirectionSource, Vector3.up);



        // Ajusta el valor de stereoPan en el AudioSource

        float stereoPan = angle / 180f;

        GetComponent<AudioSource>().panStereo = stereoPan;
        
        if (isLinkToAnotherReference)
        {
            transform.rotation = linkTransformReference.rotation;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position,transform.forward * raycastLength);
    }
}
