using System;
using Unity.Netcode;
using UnityEngine;

public class MultiplayerButton : MonoBehaviour
{
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
            default:
                Debug.LogError($"{connectionOption} is not a valid connectionoption");
                break;
        }
        gameObject.SetActive(false);
    }


}

