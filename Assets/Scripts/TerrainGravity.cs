using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGravity : MonoBehaviour
{
    [SerializeField] private float timeMultiplier = 100f;
    private TimedTrailRenderer[] trails;
    private Texture2D snowHeightMap;

    public int imageSize = 1024;
    public int terrainSize = 128;

    // Start is called before the first frame update
    void Start()
    {
        trails = FindObjectsOfType<TimedTrailRenderer>();
        snowHeightMap = FindObjectOfType<Terrain>().terrainData.alphamapTextures[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, getHeight(), transform.position.z);
    }

    float getHeight()
    {
        float y = 0;
        bool hasTrail = false;
        float minHeight = 0f;

        for (int i = 0; i < trails.Length; i++)
        {
            hasTrail = trails[i].getHeight(transform.position, out y);
            if (hasTrail && minHeight > y)
                minHeight = y;
        }


        float redAddition = 0;
        for (int i = -1; i < 2; i++)
            for (int j = -1; j < 2; j++)
            {
                Color positionColor = snowHeightMap.GetPixel((int)(transform.position.x * imageSize / terrainSize)+i, (int)(transform.position.z * imageSize / terrainSize)+j);
                redAddition += positionColor.r;
            }
        redAddition /= 9;

        return hasTrail ? Terrain.activeTerrain.SampleHeight(transform.position) + GlobalSnow.GlobalSnowOffset * (y) * redAddition : Terrain.activeTerrain.SampleHeight(transform.position) + GlobalSnow.GlobalSnowOffset * redAddition;
    }
}