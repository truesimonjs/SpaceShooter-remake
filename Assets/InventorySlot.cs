using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public int index = -1;
    private void Start()
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name);
       int siblingIndex = eventData.pointerDrag.transform.GetSiblingIndex();
    }
}
