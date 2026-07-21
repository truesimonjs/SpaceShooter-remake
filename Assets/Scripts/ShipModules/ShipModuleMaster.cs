using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.EventSystems;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ShipModuleMaster : MonoBehaviour, IDamageModify
{
    public event EventHandler OnEnergyChange;
    [field:SerializeField] public float MaxEnergy { get; private set; } = 100;
    [SerializeField] public float energy { get; set; } = 0;
    private ShipShieldModule shield;
    private StorageModule storageModule;
    public event EventHandler<ShipShieldModule> OnEquipShield;
    private void Awake()
    {
        energy = Mathf.Clamp(energy , 0, MaxEnergy);
    }
    public float TryTakeDamage(float damage = 1)
    {
        if (shield != null) damage = shield.TryTakeDamage(damage);
        return damage;
        
    }
    public bool PickupModule(ShipModuleScriptable moduleSo)
    {
        if (storageModule == null) storageModule = (StorageModule)GetModuleComponent(typeof (StorageModule));
        ShipModuleScriptable leftover = EquipModule(moduleSo, false);
        if (leftover != null)
        {
           return  storageModule.store(leftover);
        }
        return true;
    }
    public ShipModuleScriptable EquipModule(ShipModuleScriptable moduleSO, bool doSwap = true)
    {
        ShipModule module = GetModuleComponent(moduleSO.Type);
        ShipModuleScriptable oldModuleSO = module.GenericStats;
        if (oldModuleSO != null && !doSwap) return moduleSO;
        module.GenericStats = moduleSO;

        if (typeof(Module) == typeof(ShipShieldModule)) 
        {
            shield = (ShipShieldModule)module;
            OnEquipShield?.Invoke(this, shield);
        }
        return oldModuleSO;
    }

    public ShipModule GetModuleComponent(Type t)
    {
        Component component;
        if (TryGetComponent(t, out component))
        {

        }
        else
        {
            component = gameObject.AddComponent(t);
        }
        ShipModule module = component as ShipModule;
        return module;
    }
    public bool ChangeEnergyBool(float value)
    {
        float oldValue = energy;
        bool hasEnough = MaxEnergy >= energy+value && energy + value >= 0;
        if (!hasEnough) return false;
        energy += value;
        OnEnergyChange?.Invoke(this, EventArgs.Empty);
        return true;
     
    }
}
