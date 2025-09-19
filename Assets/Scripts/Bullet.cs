using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > 10) Destroy(gameObject);
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable Damageable = collision.gameObject.GetComponent<IDamageable>();
        
        if (Damageable == null) return;
        Damageable.TakeDamage();
        Destroy(gameObject);
    }



}
