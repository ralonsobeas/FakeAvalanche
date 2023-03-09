using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGravity : MonoBehaviour
{
    [SerializeField] private float timeMultiplier = 100f;
    private TimedTrailRenderer[] trails;

    // Start is called before the first frame update
    void Start()
    {
        trails = FindObjectsOfType<TimedTrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,getHeight(),transform.position.z);
    }

    float getHeight()
    {
        int i = 0;
        float y = 0;
        bool hasTrail = false;

        while(i < trails.Length)
        {
            hasTrail = trails[i].getHeight(transform.position, out y);
            if (hasTrail)
                break;

            i++;
        }
        print((1 - y));
        return hasTrail ? Terrain.activeTerrain.SampleHeight(transform.position) + GlobalSnow.GlobalSnowOffset * (1 - y): Terrain.activeTerrain.SampleHeight(transform.position) + GlobalSnow.GlobalSnowOffset;
    }
}
