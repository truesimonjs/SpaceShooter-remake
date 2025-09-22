using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    Vector3 origin;
    public GameObject owner;
    private void Start()
    {
        origin= transform.position;
     
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, origin) > 50) Destroy(gameObject);
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (!IsOwner) return;
        
        if (collision.gameObject == owner) return;
        IDamageable Damageable = collision.gameObject.GetComponent<IDamageable>();
        if (Damageable == null) return;
        
        if(NetworkManager.IsServer)Damageable.TakeDamage();
        //Damageable.TakeDamage();
        Destroy(gameObject);
        
      
        
        //DestroyRpc();
    }
    



}
