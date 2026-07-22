using System;
using UnityEngine;


public class GeneratorModule : ShipModule
{
 
 
    float timer = 0;
    [field: SerializeField] public GeneratorModuleScriptable Stats { get; private set; }
    public override ShipModuleScriptable GenericStats { get => Stats; set => Stats = value as GeneratorModuleScriptable; }


    private void Update()
    {
        const float cd = 1;
        timer -= Time.deltaTime;
        if (timer <= 0) 
        {
            core.ChangeEnergyBool(Stats.energyRate);
            timer = cd;
        }
    }
}

