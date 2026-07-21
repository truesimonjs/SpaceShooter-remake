using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ShipShieldModule : ShipModule,IDamageAbleModule
{
    //setup
    [field: SerializeField] public int Priority {  get; set;}
    public override ShipModuleScriptable GenericStats { get => Stats; set => Stats = value as ShieldShipScriptable; }
    [field: SerializeField] public ShieldShipScriptable Stats { get; private set; }
    
    //public float maxShields = 10;
    //public float shieldsPerSec = 1;
    //public float CostPerShield = 1;
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
        master.transform.parent.GetComponentInChildren<EntityHealthBar>().ShieldStart(this);
    }
    private void Update()
    {
        
        const float cooldown = 1;
        timer -= Time.deltaTime;
        if (Shields != Stats.maxShields && timer <= 0)
        {

            for (int i = 0; i < Stats.shieldsPerSec; i++)
            {
                if (Shields >= Stats.maxShields) break;
               
                if (master.ChangeEnergyBool(-Stats.CostPerShield)) Shields += 1;
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
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Modules/ShieldModule", order = 1)]
public class ShieldShipScriptable : ShipModuleScriptable
{
    public override Type Type => typeof(ShipShieldModule);
    public float maxShields = 10;
    public float shieldsPerSec = 1;
    public float CostPerShield = 1;
}
