using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Modules/ShieldModule", order = 1)]
public class ShieldShipScriptable : ShipModuleScriptable
{
    public override Type Type => typeof(ShipShieldModule);
    public float maxShields = 10;
    public float shieldsPerSec = 1;
    public float CostPerShield = 1;
}
