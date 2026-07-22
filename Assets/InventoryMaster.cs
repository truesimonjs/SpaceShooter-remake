using UnityEngine;
using static UnityEditor.Progress;

public class InventoryMaster : MonoBehaviour
{
    public GameObject slotPrefab;
    public GameObject StorageGrid;
    public GameObject EquippedGrid;
    public ShipModuleMaster moduleMaster;
    public StorageModule storageModule;


    private void Start()
    {
       
        for (int i = 0; i < storageModule.modules.Count; i++)
        {
            GameObject slot = Instantiate(slotPrefab, StorageGrid.transform);
            slot.GetComponent<InventorySlot>().index = i;
        }
        
        for (int i = 0; i < moduleMaster.AllShipModuleSO.Length; i++)
        {
            GameObject slot = Instantiate(slotPrefab, StorageGrid.transform);
            slot.GetComponent<InventorySlot>().index = i;
        }
    }

}



