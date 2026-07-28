using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 origin;
    public GameObject owner;
    public float range = 50;
    private void Start()
    {
        origin = transform.position;

    }
    void Update()
    {
        if (Vector3.Distance(transform.position, origin) > 50) Destroy(gameObject);
    }
    public void Move(Vector2 direction, float speed)
    {
        GetComponent<Rigidbody>().AddForce(direction.normalized * speed, ForceMode.Impulse);
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
