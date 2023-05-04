using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField][Tooltip("Debe ser el mismo en todos los wallet")] private int initialBalance = 10;
    private static int coins;

    private void Awake()
    {
        coins = PlayerPrefs.GetInt("wallet", initialBalance);
    }

    public static bool CanAffordMe(int price) => price <= coins;

    public static void Pay(int price)
    {
        if (!CanAffordMe(price)) throw new UnityException("Se supone que no puedes pagar esto");
        coins -= price;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("wallet", coins);
    }
}
