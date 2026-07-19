using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class ShipModule : MonoBehaviour
{
    protected ShipModuleMaster master;
    public virtual void Start()
    {
        master = GetComponentInParent<ShipModuleMaster>();
        master.EquipModule(this);
    }
}
