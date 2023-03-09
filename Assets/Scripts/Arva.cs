using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Arva : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;

    [SerializeField]
    private Transform victimPos;

    [SerializeField]
    private Text arvaNumber;

    [SerializeField]
    private Image arvaArrow;

    [SerializeField]
    private Sprite[] arrows;

    private bool power = true;
    private float actualAngle;

    private void Start()
    {
        arvaArrow.sprite = arrows[0];
        arvaNumber.text = "99999";
    }

    private void Update()
    {
        if (power) updateArva();
    }

    private void updateArva()
    {
        arvaNumber.text = ((int)Mathf.Round(Vector3.Magnitude(victimPos.position - playerPos.position))%99999).ToString();
        actualAngle = Vector3.Angle(playerPos.forward, new Vector3(victimPos.position.x, playerPos.position.y, victimPos.position.z) - playerPos.position);

        //if (actualAngle < 0) actualAngle = 360f + actualAngle;

        if (actualAngle >= -22.5 && actualAngle < 22.5) arvaArrow.sprite = arrows[0];

        else if (actualAngle >= 22.5 && actualAngle < 67.5)
        {
            arvaArrow.sprite = arrows[1];
            arvaArrow.rectTransform.rotation = new Quaternion(0, 180, 0, 1);
        }
        else if (actualAngle >= 67.5 && actualAngle < 112.5)
        {
            arvaArrow.sprite = arrows[2];
            arvaArrow.rectTransform.rotation = new Quaternion(0, 180, 0, 1);
        }
        else if (actualAngle >= 112.5 && actualAngle < 157.5)
        {
            arvaArrow.sprite = arrows[1];
            arvaArrow.rectTransform.rotation = new Quaternion(0, 0, 180, 1);
        }
        else if ((actualAngle >= 157.5 && actualAngle <= 180) || (actualAngle >= -180 && actualAngle < -157.5))
        {
            arvaArrow.sprite = arrows[0];
            arvaArrow.rectTransform.rotation = new Quaternion(0, 0, 180, 1);
        }
        else if (actualAngle >= -157.5 && actualAngle < -112.5)
        {
            arvaArrow.sprite = arrows[1];
            arvaArrow.rectTransform.rotation = new Quaternion(180, 0, 0, 1);
        }
        else if (actualAngle >= -112.5 && actualAngle < -67.5) arvaArrow.sprite = arrows[2];

        else if (actualAngle >= -67.5 && actualAngle < -22.5) arvaArrow.sprite = arrows[1];

        else arvaArrow.sprite = null;
    }
}
