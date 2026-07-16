using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IDamageable
{

    [SerializeField] protected float maxHealth = 100;
    protected Slider healthSlider;
    private float health = 100;
    public event EventHandler OnTakeDamage;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            if (healthSlider == null) return;
            healthSlider.value = health;
            healthSlider.gameObject.SetActive(health < maxHealth);
        }
    }


    private void Start()
    {

        healthSlider = GetComponentInChildren<Slider>();
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            Health = maxHealth;
        }


    }

    private void LateUpdate()
    {
        if (healthSlider == null) return;
        healthSlider.transform.parent.rotation = Quaternion.identity;
    }


    public void TakeDamage(float damage = 1)
    {

        Health -= damage;
        OnTakeDamage?.Invoke(this, EventArgs.Empty);
        if (Health <= 0) Destroy(gameObject, 0.1f);
    }


}
