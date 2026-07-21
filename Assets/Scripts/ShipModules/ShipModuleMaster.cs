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
    public ShipModule[] AllShipModule = new ShipModule[5];
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
        if (storageModule == null) storageModule = GetComponent<StorageModule>();
        ShipModuleScriptable leftover = EquipModule(moduleSo);
        if (leftover != null)
        {
            if (storageModule == null) return false;
           return  storageModule.store(leftover);
        }
        return true;
    }
    public ShipModuleScriptable EquipModule(ShipModuleScriptable moduleSo,int? targetIndex = null)
    {
        

        if (targetIndex != null)
        {
            return EquipModuleOnSlot(moduleSo, targetIndex.Value);


        }
        for (int i = 0; i < AllShipModule.Length; i++)
        {
           
            if (AllShipModule[i] == null)
            {
                return EquipModuleOnSlot(moduleSo,i);
            }

        }
        return moduleSo;
    }
    private ShipModuleScriptable EquipModuleOnSlot(ShipModuleScriptable moduleSo, int targetIndex)
    {
        if (moduleSo.IsUnique && CheckForExistingModule(moduleSo)) return moduleSo;
        ShipModuleScriptable oldModuleSO = AllShipModule[targetIndex]?.GenericStats;
        if (AllShipModule[targetIndex] != null)
        {
            Destroy(AllShipModule[targetIndex]);
        }
        AllShipModule[targetIndex] = (ShipModule)gameObject.AddComponent(moduleSo.Type); ;
        AllShipModule[targetIndex].GenericStats = moduleSo;
        if (moduleSo.Type == typeof(ShipShieldModule)) 
        {
            shield = (ShipShieldModule)AllShipModule[targetIndex];
            OnEquipShield?.Invoke(this, shield);
        }
        return oldModuleSO;
    }
    private bool CheckForExistingModule(ShipModuleScriptable moduleSO)
    {
        foreach (ShipModule module in AllShipModule)
        {
            if (moduleSO.Type == module?.GetType()) return true;
        }
        return false;
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
