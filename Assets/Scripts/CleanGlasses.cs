using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanGlasses : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("HandController"))
            healthManager.CleanGlasses();
    }
}
