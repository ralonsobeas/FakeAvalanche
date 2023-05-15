using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;

public class LocomotionManager : MonoBehaviour
{

    public GameObject rayTeleport;
    //private InputData _inputData;
    public InputActionReference toggleReference = null;

    private TeleportationProvider _teleportationProvider;
    private ContinuousMoveProviderBase _continuousMoveProviderBase;

    int locomotionValue = 1;
    // Start is called before the first frame update
    void Start()
    {
        _teleportationProvider = GetComponent<TeleportationProvider>();
        _continuousMoveProviderBase = GetComponent<ContinuousMoveProviderBase>();

        //_inputData = GetComponent<InputData>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        _inputData._leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool press);
        if (press==true)
        {
            Debug.Log("PRESS: " + press);
            SwitchLocation();
            
        }
        */
    }

    private void Awake()
    {
        toggleReference.action.started += SwitchLocation;
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= SwitchLocation;

    }

    private void SwitchLocation(InputAction.CallbackContext context)
    {
        if(locomotionValue == 0)
        {
            DisableTeleport();
            EnableContinuous();
            locomotionValue = 1;

        }
        else if (locomotionValue == 1)
        {
            DisableContinuous();
            EnableTeleport();
            locomotionValue = 0;
        }
    }

    private void DisableTeleport()
    {
        rayTeleport.SetActive(false);
        _teleportationProvider.enabled = false;

    }

    private void EnableTeleport()
    {
        rayTeleport.SetActive(true);
        _teleportationProvider.enabled = true;

    }

    private void DisableContinuous()
    {
        _continuousMoveProviderBase.enabled = false;

    }

    private void EnableContinuous()
    {
        _continuousMoveProviderBase.enabled = true;

    }
}


