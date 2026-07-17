using System;
using UnityEngine;

public class ModulePickup : MonoBehaviour
{
    //[SerializeField] private GameObject Module;
    [SerializeField] private string typeString;
    [SerializeField] Type t;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggerenter");
       GameObject other = collision.attachedRigidbody.gameObject; 
        if (other.CompareTag("Player"))
        {
           GameObject moduleHolder = other.GetComponentInChildren<ShipModuleMaster>().gameObject;

            Type t = Type.GetType(typeString);
            moduleHolder.AddComponent(t);
        }
    }

    private void OnValidate()
    {
        if (typeString != null && Type.GetType(typeString) == null) Debug.LogWarning("typestring does not point to a component", this);
    }
}
