using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ShipModuleMaster : MonoBehaviour
{
    public event EventHandler OnEnergyChange;
    [field:SerializeField] public float MaxEnergy { get; private set; } = 100;
    [SerializeField]private float energy = 100;
    private void Awake()
    {
        energy = Mathf.Clamp(energy , 0, MaxEnergy);
    }
    public float ChangeEnergyFloat(float value)
    {
        Debug.LogWarning("changeenergyfloat is deprecating");
        float oldValue = energy;
        energy = Mathf.Clamp(energy + value, 0, MaxEnergy);
        OnEnergyChange?.Invoke(this, EventArgs.Empty);
        
       //Debug.Log(energy - oldValue);
        return energy - oldValue;
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
