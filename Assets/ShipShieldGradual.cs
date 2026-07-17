using System;
using UnityEngine;

public class ShipShieldGradual : DamageAbleModule
{
    //setup
    public float maxShields = 10;
    public float shieldsPerSec = 1;
    public float CostPerShield = 1;
    //
    public event EventHandler OnShieldChange;
    public float shields = 0;
    public float Shields
    {
        get =>shields; 
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
    private ShipModuleMaster master;
    private void Start()
    {
        master = GetComponent<ShipModuleMaster>();
    }
    private void Update()
    {
        const float cooldown = 1;
        timer -= Time.deltaTime;
        if ( Shields != maxShields &&timer <= 0)
        {
            float newShield = Mathf.Clamp(Shields + shieldsPerSec, 0, maxShields);
            float shieldChange = newShield - Shields;
            float cost = master.ChangeEnergyFloat(-shieldChange * CostPerShield);
            
            Shields += -cost / CostPerShield;
            timer = cooldown;
        }
    }
    public override float TryTakeDamage(float damage = 1)
    {
        Shields -= damage;
        if (shields > 0) 
        {
            damage = 0;
     
        } else
        {
            damage = -shields;
            shields = 0;
        }
        return damage;
    }
}
