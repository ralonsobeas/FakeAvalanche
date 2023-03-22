using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //TODO change in future to glasses property and allows inventary
    public GameObject[] glasses_image_states; 
    public int numDamage = 0;
    // secs to clean glasses
    public int timeToCleanGlasses = 300;
    void Start()
    {
        ((GameManager)GameManager.Instance).OnGameStart += EquipGlasses;
        ((GameManager)GameManager.Instance).StartGame();
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
        HideAllGlasses();
        UnHidePositionGlasses(numDamage);
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
}