using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IDamageable
{

    [SerializeField] protected float maxHealth = 100;
    protected Slider healthSlider;
    private float health = 100;
    Color normalColor;
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


    private void Awake()
    {
        normalColor = GetComponent<SpriteRenderer>().color;
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
        StartCoroutine(DamageEffect());
        if (Health <= 0) Destroy(gameObject, 0.1f);
    }

    public IEnumerator DamageEffect()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();


        renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.color = normalColor;

    }


}
