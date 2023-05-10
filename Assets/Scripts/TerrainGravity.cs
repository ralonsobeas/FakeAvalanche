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
    public float characterOffset = 0;

    [SerializeField] Terrain stabilityTerrain;
    [SerializeField] GameObject lineTrailHole;
    [SerializeField] float maxStepHeight = 5f;
    public float MaxStepHeight { get => maxStepHeight; }

    private bool meCaio = false;

    // Start is called before the first frame update
    void Start()
    {
        trails = FindObjectsOfType<TimedTrailRenderer>();
        snowHeightMap = FindObjectOfType<Terrain>().terrainData.alphamapTextures[0];
    }

    // Update is called once per frame
    void Update()
    {
        print("Terrain Stability: ");
        print(stabilityTerrain.SampleHeight(transform.position) > 10f);
        if (!meCaio && transform.position.y - getHeight() > maxStepHeight && stabilityTerrain.SampleHeight(transform.position) < 10f)
        {
            Instantiate(lineTrailHole, transform.position, Quaternion.identity);
            ((GameManager)GameManager.Instance).DecreaseLife();
        }
        meCaio = stabilityTerrain.SampleHeight(transform.position) < 10f;
        transform.position = new Vector3(transform.position.x, getHeight(), transform.position.z);
    }

    

    public float getHeight()
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
        if (stabilityTerrain.SampleHeight(transform.position) < 10f)
            return Terrain.activeTerrain.SampleHeight(transform.position);
        return hasTrail ? Terrain.activeTerrain.SampleHeight(transform.position) + GlobalSnow.GlobalSnowOffset * (y) * redAddition + characterOffset : Terrain.activeTerrain.SampleHeight(transform.position) + GlobalSnow.GlobalSnowOffset * redAddition + characterOffset;
    }
}