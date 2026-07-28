using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public int index = -1;
    public InventoryMaster inventoryMaster;
    public bool isEquippedSlot;
    public ShipModuleScriptable ModuleSO
    { 
        set 
        {
            if (transform.childCount == 0) return;
            if (value != null)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = value.icon;
                transform.GetChild(0).GetComponent<Image>().color = value.icon== null? Color.red: Color.white; 
            }
                
                
           
        } 
    }
    private void Start()
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
       
       InventorySlot otherSlot = eventData.pointerDrag.GetComponent<DragableItem>().parent.GetComponent<InventorySlot>();
      if (isEquippedSlot && !otherSlot.isEquippedSlot)
        {
            inventoryMaster.slotDragged(index, otherSlot.index);
        }
    }
}
