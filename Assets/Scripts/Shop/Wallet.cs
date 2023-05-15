using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    [SerializeField][Tooltip("Debe ser el mismo en todos los wallet")] private int initialBalance = 10;
    [SerializeField] private TextMeshProUGUI moneyDisplay;
    private static Wallet Instance;
    private static int coins;

    private void Awake()
    {
        coins = PlayerPrefs.GetInt("wallet", initialBalance);
        moneyDisplay.text = coins + "€";
        Instance = this;
    }

    public static bool CanAffordMe(int price) => price <= coins;

    public static void Pay(int price)
    {
        if (!CanAffordMe(price)) throw new UnityException("Se supone que no puedes pagar esto");
        coins -= price;
        Instance.moneyDisplay.text = coins + "€";
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("wallet", coins);
    }
}
