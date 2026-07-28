using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ShipCore : MonoBehaviour, IDamageModify
{

    public int startingCapacity = 1;
    private ShipModuleMaster moduleMaster;
    private void Awake()
    {
        energy = Mathf.Clamp(energy, 0, MaxEnergy);
    }
    private void Start()
    {


        moduleMaster = GetComponent<ShipModuleMaster>();
        if (moduleMaster == null) return;
        moduleMaster.OnModuleChange += ModuleMaster_OnModuleChange;
        ModuleMaster_OnModuleChange(this, EventArgs.Empty);
    }

    private void ModuleMaster_OnModuleChange(object sender, EventArgs e)
    {
        MaxShieldUpdate();
        StorageCapacityUpdate();
    }
    //Storage
    public List<ShipModuleScriptable> storedModules = new();
    public int StorageCapacity {  get; private set; }
    public bool StoreModule(ShipModuleScriptable module)
    {
        if (storedModules.Count < StorageCapacity)
        {
            storedModules.Add(module);
            return true;
        }
        return false;
    }
    public void StorageCapacityUpdate()
    {
        StorageCapacity = startingCapacity;
        foreach (var item in moduleMaster.AllShipModuleSO)
        {
           
            if (item == null) continue;
            if (item.GetType() == typeof(StorageSO))
            {
                StorageSO storageSO = (StorageSO)item;
                StorageCapacity += storageSO.Capacity;
            }
        }
       
    }
    // energy
    public event EventHandler OnEnergyChange;
    [field: SerializeField] public float MaxEnergy { get; private set; } = 100;
    [SerializeField] public float energy { get; set; } = 0;
    public bool ChangeEnergyBool(float value)
    {
        float oldValue = energy;
        bool hasEnough = CheckEnergy(value);
        if (!hasEnough) return false;
        energy += value;
        OnEnergyChange?.Invoke(this, EventArgs.Empty);
        return true;

    }
    public bool CheckEnergy(float value)
    {
        float oldValue = energy;
        bool hasEnough = MaxEnergy >= energy + value && energy + value >= 0;
        return hasEnough;

    }
    //shields
    public event EventHandler OnShieldChange;
    public float MaxShields { get; private set; }
    private float shields = 0;
    public float Shields
    {
        get => shields;
        set
        {
            value = value >MaxShields? MaxShields:value;

            if (value != shields)
            {
                shields = value;
                OnShieldChange?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public void MaxShieldUpdate()
    {
        MaxShields = 0;
        foreach (var item in moduleMaster.AllShipModuleSO)
        {
            if (item == null) continue;
            if (item.GetType() == typeof(ShieldShipScriptable))
            {
                ShieldShipScriptable shield = (ShieldShipScriptable)item;
                MaxShields += shield.maxShields;
            }
        }
        Shields = Shields;
    }

    public float TryTakeDamage(float damage = 1)
    {
        Shields -= damage;
        if (Shields > 0)
        {
            damage = 0;
        }
        else
        {
            damage = -Shields;
            Shields = 0;
        }
       
        return damage;
    }
}
