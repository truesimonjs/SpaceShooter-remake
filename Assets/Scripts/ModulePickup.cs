using System;
using UnityEngine;

public class ModulePickup : MonoBehaviour
{
    //[SerializeField] private GameObject Module;
    [SerializeField] private ShipModuleScriptable moduleStats;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = moduleStats.icon;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggerenter");
       GameObject other = collision.attachedRigidbody.gameObject; 
        if (other.CompareTag("Player"))
        {
           ShipModuleMaster moduleMaster = other.GetComponentInChildren<ShipModuleMaster>();
            if (moduleMaster.PickupModule(moduleStats)) Destroy(gameObject);


        }
    }
    private void OnValidate()
    {
      // if (GetComponent<SpriteRenderer>().sprite != moduleStats.icon) GetComponent<SpriteRenderer>().sprite = moduleStats.icon;
    }


}
