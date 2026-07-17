using System;
using UnityEngine;

public class ModulePickup : MonoBehaviour
{
    //[SerializeField] private GameObject Module;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggerenter");
       GameObject other = collision.attachedRigidbody.gameObject; 
        if (other.CompareTag("Player"))
        {
           GameObject moduleHolder = other.GetComponentInChildren<ShipModuleMaster>().gameObject;

            GameObject module = GetComponentInChildren<ShipModule>(true).gameObject;
            module.transform.parent = moduleHolder.transform;
            module.transform.localPosition = Vector3.zero;
            module.SetActive(true);
            Destroy(this);
        }
    }


}
