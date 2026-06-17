using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IDamageable
{

    [SerializeField] protected float maxHealth = 100;
    private float health = 100;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            HealthChange();
        }
    }



    protected Slider healthSlider;
    /*
    public override void OnNetworkSpawn()
    {

        healthVariable.OnValueChanged += HealthChange;
        NetworkObject.DontDestroyWithOwner = true;
        healthSlider = GetComponentInChildren<Slider>();
        if (IsServer) healthVariable.Value = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = healthVariable.Value;
            healthSlider.gameObject.SetActive(healthVariable.Value < maxHealth);
        }
    } */
    private void Start()
    {

        healthSlider = GetComponentInChildren<Slider>();
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
            healthSlider.gameObject.SetActive(health < maxHealth);
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

    private void HealthChange()
    {

        if (healthSlider == null) return;
        healthSlider.value = health;
        healthSlider.gameObject.SetActive(health < maxHealth);

    }

    public IEnumerator DamageEffect()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }


}
