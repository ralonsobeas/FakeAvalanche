using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightGame : MonoBehaviour
{
    private bool isOn = false;

    [SerializeField]
    private GameObject light;

    public void toggle() {
        isOn = !isOn; 
        light.SetActive(isOn);
    }
}
