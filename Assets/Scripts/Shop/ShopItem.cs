using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int price;

    [SerializeField] private GameObject cantAffordFeedback;
    [SerializeField] private TMPro.TextMeshProUGUI priceLabel;

    private static List<string> items = new List<string>();
    private bool instantDestroy = false;

    private void Awake()
    {
        if (items.Contains(name))
        {
            instantDestroy = true;
            Destroy(gameObject);
        }
        else
            items.Add(name);
    }

    // Start is called before the first frame update
    void Start()
    {
        priceLabel.text = price + "€";
        if (PlayerPrefs.GetInt(name, 0) == 1)
            Sold();
        else if (!Wallet.CanAffordMe(price))
            CantAffordMe();
    }

    public void Buy()
    {
        if (!enabled) return;
        Wallet.Pay(price);
        Sold();
    }

    private void Sold()
    {
        PlayerPrefs.SetInt(name, 1);
        //Destroy(this);
        enabled = false;
        priceLabel.fontStyle = TMPro.FontStyles.Strikethrough;
        priceLabel.color = Color.red;
    }

    private void CantAffordMe()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        if (cantAffordFeedback != null)
            cantAffordFeedback.SetActive(true);
    }

    private void OnDestroy()
    {
        //GetComponent<XRGrabInteractable>().selectEntered;
        if (!instantDestroy && items.Contains(name))
            items.Remove(name);
    }

    [ContextMenu("Reset")]
    private void ResetShop()
    {
        PlayerPrefs.SetInt(name, 0);
    }
}
