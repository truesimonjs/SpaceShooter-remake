using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    Vector3 origin;
    private void Start()
    {
        origin= transform.position;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, origin) > 50) DestroyServerRpc();
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable Damageable = collision.gameObject.GetComponent<IDamageable>();
        
        if (Damageable == null) return;
        Damageable.TakeDamage();
        DestroyServerRpc();
    }
    [ServerRpc]
    public void DestroyServerRpc()
    {
        GetComponent<NetworkObject>().Despawn(true);
    }



}
