using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Arva : MonoBehaviour
{
    protected Transform playerPos;

    protected Transform victimPos;

    [SerializeField]
    private Text arvaNumber;

    [SerializeField]
    private Image arvaArrow;

    [SerializeField]
    private Sprite[] arrows;

    protected bool power = false;
    private float actualAngle;

    private void Start()
    {
        arvaArrow.sprite = arrows[0];
        arvaNumber.text = "99999";
        playerPos = transform;
        OnLoadEvent.onLoadedRefugio.AddListener(OnLoadedRefugio);
        OnLoadEvent.onLoadedMission.AddListener(OnLoadedMission);
    }

    private void OnLoadedRefugio()
    {
        enabled = false;
    }

    private void OnLoadedMission()
    {
        enabled = true;
        victimPos = GameObject.FindGameObjectWithTag("Victim").transform;
    }

    public void TogglePower() => power = !power;

    private void Update()
    {
        if (power) updateArva();
    }

    protected void updateArva()
    {
        arvaNumber.text = ((int)Mathf.Min(Mathf.Round(Vector3.Magnitude(victimPos.position - playerPos.position)),99999)).ToString();
        actualAngle = Vector3.SignedAngle(playerPos.forward, new Vector3(victimPos.position.x, playerPos.position.y, victimPos.position.z) - playerPos.position, playerPos.up);

        //if (actualAngle < 0) actualAngle = 360f + actualAngle;

        if (actualAngle >= -22.5 && actualAngle < 22.5) arvaArrow.sprite = arrows[0];

        else if (actualAngle >= 22.5 && actualAngle < 67.5) arvaArrow.sprite = arrows[1];

        else if (actualAngle >= 67.5 && actualAngle < 112.5) arvaArrow.sprite = arrows[2];

        else if (actualAngle >= 112.5 && actualAngle < 157.5) arvaArrow.sprite = arrows[3];

        else if ((actualAngle >= 157.5 && actualAngle <= 180) || (actualAngle >= -180 && actualAngle < -157.5)) arvaArrow.sprite = arrows[4];

        else if (actualAngle >= -157.5 && actualAngle < -112.5) arvaArrow.sprite = arrows[5];

        else if (actualAngle >= -112.5 && actualAngle < -67.5) arvaArrow.sprite = arrows[6];

        else if (actualAngle >= -67.5 && actualAngle < -22.5) arvaArrow.sprite = arrows[7];

        else arvaArrow.sprite = null;
    }
}
