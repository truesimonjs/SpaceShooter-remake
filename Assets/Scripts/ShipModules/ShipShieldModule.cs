using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ShipShieldModule : ShipModule
{
    //setup
    [field: SerializeField] public int Priority {  get; set;}
    public override ShipModuleScriptable GenericStats { get => Stats; set => Stats = value as ShieldShipScriptable; }
    [field: SerializeField] public ShieldShipScriptable Stats { get; private set; }

    

    private float timer = 0;
    public override void Start()
    {
        base.Start();
        core = GetComponent<ShipCore>();
    }
    private void Update()
    {
        
        const float cooldown = 1;
        timer -= Time.deltaTime;
        if (core.Shields != core.MaxShields && timer <= 0)
        {

            for (int i = 0; i < Stats.shieldsPerSec; i++)
            {
                if (core.Shields >= core.MaxShields) break;
               
                if (core.ChangeEnergyBool(-Stats.CostPerShield)) core.Shields += 1;
            }
            timer = cooldown;
        }
    }
}

