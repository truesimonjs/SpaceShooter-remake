using UnityEngine;
using System.Collections;
using Unity.Netcode;

public class Entity : NetworkBehaviour, IDamageable
{

    [SerializeField] private float health = 5;
    [SerializeField]private NetworkVariable<float> healthVariable = new NetworkVariable<float>(5,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
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
    [Rpc(SendTo.Owner)]
    public void TakeDamageRpc(float damage = 1)
    {
        healthVariable.Value -= damage;
        //health -= damage;
        DamageEffectRpc();
        if (health <= 0) Destroy(gameObject, 0.1f);
    }
    [Rpc(SendTo.Everyone)]
    public void DamageEffectRpc()
    {
        StartCoroutine(DamageEffect());
    }
    
    public IEnumerator DamageEffect()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }
}
