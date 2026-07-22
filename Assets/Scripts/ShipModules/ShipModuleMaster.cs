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
    [SerializeField] private ShipModuleScriptable[] AllStarterModules;
    private ShipModule[] AllShipModuleComp = new ShipModule[5];
    private void Awake()
    {
        energy = Mathf.Clamp(energy , 0, MaxEnergy);
        //this feels like a band aid but it should let ships understand the modules you put on before game start
        for (int i = 0; i < AllStarterModules.Length; i++)
        {
            if (AllStarterModules[i] != null) EquipModuleOnSlot(AllStarterModules[i], i);
        }
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
        for (int i = 0; i < AllShipModuleComp.Length; i++)
        {
           
            if (AllShipModuleComp[i] == null)
            {
                return EquipModuleOnSlot(moduleSo,i);
            }

        }
        return moduleSo;
    }
    private ShipModuleScriptable EquipModuleOnSlot(ShipModuleScriptable moduleSo, int targetIndex)
    {
        if (moduleSo.IsUnique && CheckForExistingModule(moduleSo)) return moduleSo;
        ShipModuleScriptable oldModuleSO = AllShipModuleComp[targetIndex]?.GenericStats;
        //setup the component and delete the old one
        if (AllShipModuleComp[targetIndex] != null)
        {
            Destroy(AllShipModuleComp[targetIndex]);
        }
        AllShipModuleComp[targetIndex] = (ShipModule)gameObject.AddComponent(moduleSo.Type); ;
        AllShipModuleComp[targetIndex].GenericStats = moduleSo;

        //let the shieldbar know if there's a new shield in town
        if (moduleSo.Type == typeof(ShipShieldModule)) 
        {
            shield = (ShipShieldModule)AllShipModuleComp[targetIndex];
            OnEquipShield?.Invoke(this, shield);
        }
        return oldModuleSO;
    }
    private bool CheckForExistingModule(ShipModuleScriptable moduleSO)
    {
        foreach (ShipModule module in AllShipModuleComp)
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
