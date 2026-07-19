using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 origin;
    public GameObject owner;
    private void Start()
    {
        origin = transform.position;

    }
    void Update()
    {
        if (Vector3.Distance(transform.position, origin) > 50) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == null) return;
        GameObject other = collision.attachedRigidbody.gameObject;

        if (other == owner) return;
        IDamageable Damageable = other.GetComponent<IDamageable>();
        if (Damageable == null) return;

        Damageable.TakeDamage();

        Destroy(gameObject);



    }




}
