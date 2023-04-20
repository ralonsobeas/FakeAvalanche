using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArvaIntermidiate : Arva
{
    [SerializeField]
    private GameObject[] arvaDepth;

    [SerializeField]
    private float depthStep = 1;

    private void Update()
    {
        if (power)
        {
            updateArva();
            updateDepth();
        }
    }

    private void updateDepth()
    {
        float depth = ((int)Mathf.Round(Mathf.Abs(playerPos.position.y - victimPos.position.y)/depthStep));

        for(int i=0;i < arvaDepth.Length; ++i)
        {
            if(i < depth) arvaDepth[i].SetActive(true);
            else arvaDepth[i].SetActive(false);
        }
    }
}
