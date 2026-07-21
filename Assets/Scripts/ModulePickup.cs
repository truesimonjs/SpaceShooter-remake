using System;
using UnityEngine;

public class ModulePickup : MonoBehaviour
{
    //[SerializeField] private GameObject Module;
    [SerializeField] private ShipModuleScriptable moduleStats;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggerenter");
       GameObject other = collision.attachedRigidbody.gameObject; 
        if (other.CompareTag("Player"))
        {
           ShipModuleMaster moduleMaster = other.GetComponentInChildren<ShipModuleMaster>();
            moduleMaster.EquipModule(moduleStats);
            Destroy(gameObject);
            
        }
    }


}
