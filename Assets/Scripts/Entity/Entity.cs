using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IDamageable
{

    [field:SerializeField] public float MaxHealth { get; private set; } = 100;
    [SerializeField] private IDamageModify damageModify;
    private float health = 100;
    public event EventHandler OnTakeDamage;
    public event EventHandler HealthChange;
    public event EventHandler OnDeath;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            HealthChange?.Invoke(this, EventArgs.Empty);
        }
    }


    public virtual void Start()
    {
        Health = MaxHealth;
        if (damageModify == null) damageModify = GetComponentInChildren<IDamageModify>(); 
    }


    private bool dying = false;
    public virtual void TakeDamage(float damage = 1)
    {
        if (dying) return;
        OnTakeDamage?.Invoke(this, EventArgs.Empty);
        if (damageModify != null) damage = damageModify.TryTakeDamage(damage);
        
        Health -= damage;
        if (Health <= 0) 
        {
            dying = true;
            OnDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject, 0.1f);
        }
    }


}
