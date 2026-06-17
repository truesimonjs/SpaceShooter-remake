using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] bool destroyOnImpact = false;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable Damageable = collision.gameObject.GetComponent<IDamageable>();
        if (Damageable == null) return;
        Damageable.TakeDamage(damage);

    }
}
