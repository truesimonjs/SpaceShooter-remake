using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EventSystems;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ShipModuleMaster : MonoBehaviour, IDamageModify
{
    public event EventHandler OnEnergyChange;
    [field:SerializeField] public float MaxEnergy { get; private set; } = 100;
    [SerializeField] public float energy { get; set; } = 0;
    //private List<ShipShieldModule> tankModules = new();
    private ShipShieldModule shield;
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
    public void EquipModule(ShipModuleScriptable moduleObject)
    {
        Type t = moduleObject.Type;
        Component component;
        if (TryGetComponent(t, out component))
        {
           
        } else
        {
            component = gameObject.AddComponent(t);
        }

        ShipModule module = component as ShipModule;
        module.GenericStats = moduleObject;
        if (t == typeof(ShipShieldModule)) 
        {
            shield = (ShipShieldModule)module;
            OnEquipShield?.Invoke(this, shield);
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
