using System;
using Unity.Netcode;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    public GameObject spawnShipButton;
    public GameObject hostOrClientButtons;
    private void Awake()
    {
        instance = this;
    }
    public void StartGame(string connectionOption)
    {

        switch (connectionOption)
        {
            case "host":
                NetworkManager.Singleton.StartHost();
                break;
            case "client":
                NetworkManager.Singleton.StartClient();
                break;
            case "server":
                NetworkManager.Singleton.StartServer();
                break;
            default:
                Debug.LogError($"{connectionOption} is not a valid connectionoption");
                break;
        }
        hostOrClientButtons.SetActive(false);
        spawnShipButton.SetActive(true);
    }
    public void SpawnShip()
    {
        PlayerClient.instance.RequestShip();
    }

}

