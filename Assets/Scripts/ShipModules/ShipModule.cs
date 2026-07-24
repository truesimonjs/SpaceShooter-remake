using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public abstract class ShipModule : MonoBehaviour
{
    protected ShipModuleMaster master;
    protected ShipCore core;
    public abstract ShipModuleScriptable GenericStats { get; set; }
    public virtual void UseAbillity()
    {

    }
    public virtual void Start()
    {
        master = GetComponentInParent<ShipModuleMaster>();
        core  = GetComponentInParent<ShipCore>();
        //master.EquipModule(this);
    }
}

public class ShipModule<StatType> : ShipModule
    where StatType : ShipModuleScriptable
{
    public override ShipModuleScriptable GenericStats { get => Stats; set => Stats = value as StatType; }
    [field: SerializeField] public StatType Stats { get; protected set; }
}

