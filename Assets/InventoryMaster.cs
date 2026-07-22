using UnityEngine;
using static UnityEditor.Progress;

public class InventoryMaster : MonoBehaviour
{
    public static InventoryMaster instance;
    public GameObject slotPrefab;
    public GameObject StorageGrid;
    public GameObject EquippedGrid;
    public GameObject player;
    private ShipModuleMaster moduleMaster;
    private StorageModule storageModule;
    private InventorySlot[] storageSlots;
    private InventorySlot[] equippedSlots;
    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }
    private void Start()
    {
       
        moduleMaster = player.GetComponentInChildren<ShipModuleMaster>();
        equippedSlots = new InventorySlot[moduleMaster.AllShipModuleSO.Length];
        //
        storageModule = player.GetComponentInChildren<StorageModule>();
        storageSlots = new InventorySlot[storageModule.Stats.Capacity];
        for (int i = 0; i < storageSlots.Length; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, StorageGrid.transform).GetComponent<InventorySlot>();
            slot.index = i;
            storageSlots[i] = slot;
            slot.isEquippedSlot = false;
            slot.inventoryMaster = this;
        }
        
        for (int i = 0; i < equippedSlots.Length; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, EquippedGrid.transform).GetComponent<InventorySlot>();
            slot.index = i;
            equippedSlots[i] = slot;
            slot.isEquippedSlot = true;
            slot.inventoryMaster = this;
        }


    }
    public void slotDragged(int EquippedIndex, int storageIndex)
    {
       ShipModuleScriptable leftover = moduleMaster.EquipModule(storageModule.modules[storageIndex], EquippedIndex);
        storageModule.modules[storageIndex] = leftover;
    }
    private void Update()
    {
        
        for (int i = 0; i < moduleMaster.AllShipModuleSO.Length; i++)
        {
            equippedSlots[i].ModuleSO = moduleMaster.AllShipModuleSO[i];
        }
        for (int i = 0; i < storageModule.modules.Count; i++)
        {
            storageSlots[i].ModuleSO = storageModule.modules[i];
        }
    }

}



