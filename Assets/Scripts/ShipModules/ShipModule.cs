using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public abstract class ShipModule : MonoBehaviour
{
    protected ShipModuleMaster master;
    protected ShipCore core;
    public abstract ShipModuleScriptable GenericStats { get; set; }
    public virtual void Start()
    {
        master = GetComponentInParent<ShipModuleMaster>();
        core  = GetComponentInParent<ShipCore>();
        //master.EquipModule(this);
    }
}

