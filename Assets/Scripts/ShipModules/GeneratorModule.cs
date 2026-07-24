using System;
using UnityEngine;


public class GeneratorModule : ShipModule<GeneratorModuleScriptable>
{
 
 
    float timer = 0;
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

