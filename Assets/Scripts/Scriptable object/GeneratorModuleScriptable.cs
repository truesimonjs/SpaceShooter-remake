using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Modules/GeneratorModule", order = 1)]
public class GeneratorModuleScriptable : ShipModuleScriptable
{
    public override Type Type => typeof(GeneratorModule);
    public float energyRate = 1;
}

