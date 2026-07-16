using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IDamageable
{

    [SerializeField] public float MaxHealth { get; private set; } = 100;
    private float health = 100;
    public event EventHandler OnTakeDamage;
    public event EventHandler OnHealthChange;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            OnHealthChange?.Invoke(this, EventArgs.Empty);
        }
    }


    public virtual void Start()
    {
        Health = MaxHealth;
    }

 

    public virtual void TakeDamage(float damage = 1)
    {

        Health -= damage;
        OnTakeDamage?.Invoke(this, EventArgs.Empty);
        if (Health <= 0) Destroy(gameObject, 0.1f);
    }


}
