using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OVR;
using UnityEngine.SceneManagement;

public class Wallet : MonoBehaviour
{
    [SerializeField][Tooltip("Debe ser el mismo en todos los wallet")] private int initialBalance = 10;
    private TextMeshProUGUI moneyDisplay;
    private static Wallet Instance;
    private static int coins;

    public AudioClip messageClip;
    public AudioSource messageSound;
    public string nameSceneRefugio;


    private void Start()
    {

    }


    private void Awake()
    {
        OnLoadEvent.onLoadedRefugio.AddListener(OnLoadedRefugio);
    }

    private void OnLoadedRefugio()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName.Equals(nameSceneRefugio) && ScoreManager.score > 0)
        {
            messageSound.PlayOneShot(messageClip);
        }

        moneyDisplay = GameObject.FindGameObjectWithTag("Saldo").GetComponent<TextMeshProUGUI>();
        coins = PlayerPrefs.GetInt("wallet", initialBalance);
        moneyDisplay.text = coins + "€";
        Instance = this;
        coins += (int)ScoreManager.score;
    }

    public static bool CanAffordMe(int price) => price <= coins;

    [ContextMenu("Reset Wallet")]
    private void ResetWallet()
    {
        PlayerPrefs.SetInt("wallet", initialBalance);
        //ScoreManager.score = initialBalance;
    }

    public static void Pay(int price)
    {
        if (!CanAffordMe(price)) throw new UnityException("Se supone que no puedes pagar esto");
        coins -= price;
        Instance.moneyDisplay.text = coins + "€";
        //ScoreManager.score = coins;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("wallet", coins);
    }
}
