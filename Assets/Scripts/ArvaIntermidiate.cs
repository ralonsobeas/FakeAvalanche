using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArvaIntermidiate : Arva
{
    [SerializeField]
    private GameObject[] arvaDepth;

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
        float depth = ((int)Mathf.Round(Mathf.Abs(playerPos.position.y - victimPos.position.y)/3));

        for(int i=0;i < arvaDepth.Length; ++i)
        {
            if(i < depth) arvaDepth[i].SetActive(true);
            else arvaDepth[i].SetActive(false);
        }
    }
}
