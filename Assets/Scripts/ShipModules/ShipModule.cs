using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public abstract class ShipModule : MonoBehaviour
{
    protected ShipModuleMaster master;
    public abstract ShipModuleScriptable GenericStats { get; set; }
    public virtual void Start()
    {
        master = GetComponentInParent<ShipModuleMaster>();
        //master.EquipModule(this);
    }
}
public abstract class ShipModuleScriptable : ScriptableObject
{
    public abstract Type Type { get; }
}
