
using Unity.Netcode;
using UnityEngine;

public class BuildCommandLine : MonoBehaviour
{
  
    void Start()
    {
        Debug.Log("commandline debug");
        if (Application.isEditor) return;
        Debug.Log("after editor check");
        string[] args = System.Environment.GetCommandLineArgs();


        foreach (string item in args)
        {
            if (item == "-server")
            {
                Debug.Log("server command found");
                ButtonManager.instance.StartGame("server");
            }
        }
      

    }


}
