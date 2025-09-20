using UnityEngine;
using System.Collections;
using Unity.Netcode;

public class Entity : NetworkBehaviour, IDamageable
{

    [SerializeField] private float health = 5;
    [SerializeField]private NetworkVariable<float> healthVariable = new NetworkVariable<float>(5);
    public override void OnNetworkSpawn()
    {
        healthVariable.Value = 500;
    }
    private void Update()
    {
       health = healthVariable.Value;
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
    [Rpc(SendTo.Server)]
    public void TakeDamageRpc(float damage = 1)
    {
        healthVariable.Value -= damage;
        //health -= damage;
        StartCoroutine(DamageEffect());
        if (health <= 0) Destroy(gameObject, 0.1f);
    }
    
    public IEnumerator DamageEffect()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }
}
