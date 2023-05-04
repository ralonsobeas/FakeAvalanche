using System;
using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //TODO change in future to glasses property and allows inventary
    public GameObject[] glasses_image_states;
    public GameObject objectToCleanGlasses;
    public GameObject glassesImageFog;
    public int numDamage = 0;
    // secs to clean glasses
    public float timeToBlurGlasses = 60f;
    private int invokeCounter = 0;
    [Range(0, 1)] public float percentageDecreaseTime = 0.3f;
    void Start()
    {
        ((GameManager)GameManager.Instance).OnGameStart += EquipGlasses;
        ((GameManager)GameManager.Instance).StartGame();
        CallInvokeFogGlasses();
    }

    public void EquipGlasses()
    {
        HideAllGlasses();
        UnHidePositionGlasses(0);
        // Show picture glasses       
    }

    [ContextMenu("DecreaseLife")]
    public void BreakGlasses()
    {
        if (numDamage >= 3) return;
        numDamage++;
        timeToBlurGlasses *= percentageDecreaseTime;
        HideAllGlasses();
        UnHidePositionGlasses(numDamage);
        
        // change picture broken and frozen glasses
    }

    public void DestroyGlasses()
    {
        // change picture glasses
    }

    private void CallInvokeFogGlasses()
    {
        if (numDamage >= 3)
        {
            return;
        }
        invokeCounter++;
        Invoke("CountdownToCleanGlasses", timeToBlurGlasses);
    }

    public void CountdownToCleanGlasses()
    {
        invokeCounter--;
        if (invokeCounter != 0)
        {
            return;
        }
        // foggy glasses
        glassesImageFog.SetActive(true);
        CallInvokeFogGlasses();


    }

    public void CleanGlasses()
    {
        // limpiar gafas
        glassesImageFog.SetActive(false);
        CallInvokeFogGlasses();
    }


    private void HideAllGlasses()
    {
        for(int i = 0; i < glasses_image_states.Length; i++)
        {
           glasses_image_states[i].SetActive(false); 
        }
    } 
    
    private void UnHidePositionGlasses(int position)
    {
        glasses_image_states[position].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == objectToCleanGlasses.name)
        {
            CleanGlasses();
        }
    }
}