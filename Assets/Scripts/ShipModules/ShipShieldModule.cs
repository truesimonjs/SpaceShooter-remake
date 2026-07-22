using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ShipShieldModule : ShipModule,IDamageAbleModule
{
    //setup
    [field: SerializeField] public int Priority {  get; set;}
    public override ShipModuleScriptable GenericStats { get => Stats; set => Stats = value as ShieldShipScriptable; }
    [field: SerializeField] public ShieldShipScriptable Stats { get; private set; }
    //
    public event EventHandler OnShieldChange;
    public float shields = 0;
    public float Shields
    {
        get => shields;
        set
        {
            if (value != shields)
            {
                
                shields = value;
                OnShieldChange?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    

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
        if (Shields != Stats.maxShields && timer <= 0)
        {

            for (int i = 0; i < Stats.shieldsPerSec; i++)
            {
                if (core.Shields >= core.MaxShields) break;
               
                if (core.ChangeEnergyBool(-Stats.CostPerShield)) core.Shields += 1;
            }
            timer = cooldown;
        }
    }
    public float TryTakeDamage(float damage = 1)
    {
        Shields -= damage;
        if (shields > 0)
        {
            damage = 0;

        }
        else
        {
            damage = -shields;
            shields = 0;
        }
        return damage;
    }
}

