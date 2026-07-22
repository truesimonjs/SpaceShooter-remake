using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StorageModule : ShipModule
{

    public override ShipModuleScriptable GenericStats { get => Stats; set => Stats = value as StorageSO; }
    [field: SerializeField] public StorageSO Stats { get; private set; }
    public List<ShipModuleScriptable> modules = new();
    private void Awake()
    {
        modules = new List<ShipModuleScriptable>();
    }
    public bool store(ShipModuleScriptable module)
    {
        if(modules.Count < Stats.Capacity)
        {
            modules.Add(module);
            return true;
        }
        return false;
    }

}
