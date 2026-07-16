using System;
using UnityEngine;

public class ShipModuleMaster : MonoBehaviour
{
    public event EventHandler OnEnergyChange;
    [field:SerializeField] public float MaxEnergy { get; private set; } = 100;
    [SerializeField]private float energy = 100;

    public float ChangeEnergy(float value)
    {
        float oldValue = energy;
        energy = Mathf.Clamp(energy + value, 0, MaxEnergy);
        OnEnergyChange?.Invoke(this, EventArgs.Empty);
        return energy - oldValue;
    }
}
