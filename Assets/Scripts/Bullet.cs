using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 origin;
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
        IDamageable Damageable = collision.gameObject.GetComponent<IDamageable>();
        
        if (Damageable == null) return;
        Damageable.TakeDamage();
        Destroy(gameObject);
    }



}
