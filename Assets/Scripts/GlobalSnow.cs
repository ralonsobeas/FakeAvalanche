using UnityEngine;

public class GlobalSnow : MonoBehaviour
{
    [Range(0.1f, 5f)]
    public float globalSnowAmount = 0.5f;

    public float offsetMultiplier = 1f;

    private static GlobalSnow instance;

    public static float GlobalSnowOffset { get { return instance.globalSnowAmount*instance.offsetMultiplier; } }

    [Range(0.1f, 5f)]
    public float globalSnowAmountDeep = 0.5f;

    [Range(0.1f, 1f)]
    public float snowBlendStrength;


    public float offsetMultiplier = 1f;

    private static GlobalSnow instance;

    public static float GlobalSnowOffset { get { return instance.globalSnowAmount * instance.offsetMultiplier; } }


    public Color snowColorUpper;
    public Color snowColorBottom;

    public void Start()
    {
        instance = this;
        SetValues();
    }

    public void OnValidate()
    {
        SetValues();
    }

    private void FixedUpdate()
    {
        SetValues();
    }

    private void SetValues()
    {
        Shader.SetGlobalFloat("_GlobalSnowAmount", globalSnowAmount);
        Shader.SetGlobalFloat("_GlobalSnowAmountDeep", globalSnowAmountDeep);
        Shader.SetGlobalFloat("_TerrainSnowBlendStrength", snowBlendStrength);

        Shader.SetGlobalColor("_snowColorUpper", snowColorUpper);
        Shader.SetGlobalColor("_snowColorBottom", snowColorBottom);
    }
}