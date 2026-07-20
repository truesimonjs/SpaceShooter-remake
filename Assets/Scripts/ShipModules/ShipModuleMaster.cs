using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ShipModuleMaster : MonoBehaviour, IDamageModify
{
    public event EventHandler OnEnergyChange;
    [field:SerializeField] public float MaxEnergy { get; private set; } = 100;
    [SerializeField] public float energy { get; set; } = 100;
    private List<ShipShieldModule> tankModules = new();
    private List<ShipModule> modules = new();
    public event EventHandler<ShipShieldModule> OnEquipShield;
    private void Awake()
    {
        energy = Mathf.Clamp(energy , 0, MaxEnergy);
    }
    public float TryTakeDamage(float damage = 1)
    {
        foreach (var item in tankModules)
        {
           damage = item.TryTakeDamage(damage);
            if (damage <= 0) break;
        }
        return damage;
    }
    public void EquipModule(ShipModule module)
    {
        
        modules.Add(module);
        if (module.GetType() == typeof(ShipShieldModule)) 
        {
            tankModules.Add(module as ShipShieldModule);
            OnEquipShield?.Invoke(this, module as ShipShieldModule);
        }

    }
    /*
    public float ChangeEnergyFloat(float value)
    {
        Debug.LogWarning("changeenergyfloat is deprecating");
        float oldValue = energy;
        energy = Mathf.Clamp(energy + value, 0, MaxEnergy);
        OnEnergyChange?.Invoke(this, EventArgs.Empty);
        
       //Debug.Log(energy - oldValue);
        return energy - oldValue;
    }*/
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
