using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    //[SerializeField] bool destroyOnImpact = false;
    [SerializeField] float selfDamage = 10;
    [SerializeField] private float attackCD = 5f;
    float nextDamage = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (nextDamage > Time.time) return;
        IDamageable Damageable = collision.rigidbody.gameObject.GetComponent<IDamageable>();
        if (Damageable == null) return;
        Damageable.TakeDamage(damage);
        nextDamage = Time.time + attackCD;
        ShipController othersShipcontroller = collision.rigidbody.gameObject.GetComponent<ShipController>();
        if (othersShipcontroller != null) othersShipcontroller.TakeKnockback(transform.position);
        ShipController myShipController = GetComponent<ShipController>();
        myShipController.TakeKnockback(collision.rigidbody.position);
        if (selfDamage > 0)
        {
            GetComponent<IDamageable>().TakeDamage(selfDamage);
        }

    }
}
