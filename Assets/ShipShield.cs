using System;
using UnityEngine;

public class ShipShield : DamageAbleModule
{
    //setup
    public float maxShields = 10;
    public float shieldsPerSec = 1;
    public float CostPerShield = 1;
    //
    public event EventHandler OnShieldChange;
    public float shields = 0;
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
        if ( shields != maxShields &&timer <= 0)
        {
            float newShield = Mathf.Clamp(shields + shieldsPerSec, 0, maxShields);
            float shieldChange = newShield - shields;
            float cost = master.ChangeEnergy(-shieldChange * CostPerShield);
            shields += -cost / CostPerShield;
            timer = cooldown;
        }
    }
    public override float TryTakeDamage(float damage = 1)
    {
        shields -= damage;
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
