using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
    void Awake()
    {
        
        OnLoadEvent.onLoadedMission.AddListener(OnLoadedMission);
        OnLoadEvent.onLoadedRefugio.AddListener(OnLoadedRefugio);
    }

    private void OnLoadedRefugio()
    {
        //Debug.Log("Terrain establecido para refugio");
        enabled = false;
        GetComponent<CustomContinuousMoveProvider>().enabled = false;
        ActionBasedContinuousMoveProvider actionProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        actionProvider.enabled = true;
        GetComponent<CharacterControllerDriver>().locomotionProvider = actionProvider;
    }

    private void OnLoadedMission()
    {
        //Debug.Log("Terrain establecido para misión");
        enabled = true;
        bool isSnow = FindObjectOfType<TerrainSnowManager>() != null;
        CustomContinuousMoveProvider customProvider = GetComponent<CustomContinuousMoveProvider>();
        customProvider.enabled = true;
        GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
        GetComponent<CharacterControllerDriver>().locomotionProvider = customProvider;

        trails = FindObjectsOfType<TimedTrailRenderer>();
        snowHeightMap = FindObjectOfType<Terrain>().terrainData.alphamapTextures[0];
        foreach (Terrain t in FindObjectsOfType<Terrain>())
            if (t.name.Contains("Stability"))
                stabilityTerrain = t;
    }

    // Update is called once per frame
    void Update()
    {
        if (stabilityTerrain == null)
            return;
        //print("Terrain Stability: ");
        //print(stabilityTerrain.SampleHeight(transform.position) > 10f);
        //Debug.Log("Variables: meCaio=" + meCaio + ", getHeight=" + getHeight() + ", estoyEnCaio=" + (stabilityTerrain.SampleHeight(transform.position) < 10f) + ", mi Y=" + transform.position.y);
        if (!meCaio && transform.position.y - getHeight() > maxStepHeight/10f && stabilityTerrain.SampleHeight(transform.position) < 10f)
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
        Debug.Log(hasTrail ? Terrain.activeTerrain.SampleHeight(transform.position) + GlobalSnow.GlobalSnowOffset * (y) * redAddition + characterOffset : Terrain.activeTerrain.SampleHeight(transform.position) + GlobalSnow.GlobalSnowOffset * redAddition + characterOffset);
        return hasTrail ? Terrain.activeTerrain.SampleHeight(transform.position) + GlobalSnow.GlobalSnowOffset * (y) * redAddition + characterOffset : Terrain.activeTerrain.SampleHeight(transform.position) + GlobalSnow.GlobalSnowOffset * redAddition + characterOffset;
    }
}