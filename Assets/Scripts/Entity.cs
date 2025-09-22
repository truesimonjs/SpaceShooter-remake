using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Entity : NetworkBehaviour, IDamageable
{

    // [SerializeField] private float health = 5;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private NetworkVariable<float> healthVariable = new NetworkVariable<float>(5, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);



    private Slider healthSlider;
    public override void OnNetworkSpawn()
    {

        healthVariable.OnValueChanged += HealthChange;
        NetworkObject.DontDestroyWithOwner = true;
        healthSlider = GetComponentInChildren<Slider>();
        if (IsOwner) healthVariable.Value = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = healthVariable.Value;
            healthSlider.gameObject.SetActive(healthVariable.Value < maxHealth);
        }


    }
    private void LateUpdate()
    {
        if (healthSlider == null) return;
        healthSlider.transform.parent.rotation = Quaternion.identity;
    }


    public void TakeDamage(float damage = 1)
    {
        /*
        health -= damage;
        StartCoroutine(DamageEffect());
        if (health <= 0) Destroy(gameObject, 0.1f);
        */

        TakeDamageRpc(damage);


    }
    [Rpc(SendTo.Owner)]
    public void TakeDamageRpc(float damage = 1)
    {
        healthVariable.Value -= damage;
        //health -= damage;
        DamageEffectRpc();

        if (healthVariable.Value <= 0)
        {


            Invoke(nameof(Despawn), 0.1f);



        }
    }
    public void Despawn()
    {
        NetworkObject.Despawn();
    }




    [Rpc(SendTo.Everyone)]
    public void DamageEffectRpc()
    {
        StartCoroutine(DamageEffect());

    }

    public void HealthChange(float previous, float current)
    {

        if (healthSlider == null) return;
        healthSlider.value = healthVariable.Value;
        healthSlider.gameObject.SetActive(healthVariable.Value < maxHealth);

    }

    public IEnumerator DamageEffect()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }


}
