using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //TODO change in future to glasses property and allows inventary
    public int numLifes = 3;
    // secs to clean glasses
    public int timeToCleanGlasses = 300;
    void Start()
    {
        ((GameManager)GameManager.Instance).OnGameStart += EquipGlasses;
    }

    public void EquipGlasses()
    {
        // Show picture glasses       
    }

    public void BreakGlasses()
    {
        // change picture broken and frozen glasses
    }

    public void DestroyGlasses()
    {
        // change picture glasses
    }

    public void CountdownToCleanGlasses()
    {
        StartCoroutine(TimeToFogUpGlasses(timeToCleanGlasses));
    }

    IEnumerator TimeToFogUpGlasses(int secs) {
        yield return new WaitForSeconds(secs);
        // show foggy glasses
    }
}