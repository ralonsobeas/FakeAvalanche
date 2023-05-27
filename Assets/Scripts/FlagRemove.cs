using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagRemove : MonoBehaviour
{
    [SerializeField]
    private GameObject flagCloth;

    [SerializeField]
    private GameObject completeFlag;

    [SerializeField]
    private Transform cinturonParent;

    [SerializeField]
    private Vector3 initialBeltPosition;

    private bool desplegada = false;

    private bool isGrounded = false;

    public void plegarDesplegar()
    {
        desplegada = !desplegada;
        flagCloth.SetActive(desplegada);
    }

    public void grounded()
    {
        if(isGrounded)
        {
            GameObject.Instantiate(completeFlag, this.transform.position, Quaternion.identity);
            ((GameManager)GameManager.Instance).PlantFlag();
        }

        this.transform.position = cinturonParent.position + initialBeltPosition;
        this.transform.rotation = Quaternion.identity;
        this.transform.SetParent(cinturonParent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is TerrainCollider) isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is TerrainCollider) isGrounded = false;
    }
}
