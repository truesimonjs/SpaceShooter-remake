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
    public float ChangeEnergy(float value)
    {
        float oldValue = energy;
        energy = Mathf.Clamp(energy + value, 0, MaxEnergy);
        OnEnergyChange?.Invoke(this, EventArgs.Empty);
        
       //Debug.Log(energy - oldValue);
        return energy - oldValue;
    }
}
