using System;
using UnityEngine;

public class ShipShieldModule : MonoBehaviour,IDamageAbleModule
{
    //setup
    [field: SerializeField] public int Priority {  get; set;}
    public float maxShields = 10;
    public float shieldsPerSec = 1;
    public float CostPerShield = 1;
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
    private ShipModuleMaster master;
    private void Start()
    {
        master = GetComponent<ShipModuleMaster>();
        transform.parent.GetComponentInChildren<HealthBar>().ShieldStart(this);
    }
    private void Update()
    {
        
        const float cooldown = 1;
        timer -= Time.deltaTime;
        if (Shields != maxShields && timer <= 0)
        {

            for (int i = 0; i < shieldsPerSec; i++)
            {
                if (Shields >= maxShields) break;
               
                if (master.ChangeEnergyBool(-CostPerShield)) Shields += 1;
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
