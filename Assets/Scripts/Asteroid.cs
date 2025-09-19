using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    
    private void Start()
    {
        //transform.eulerAngles = new Vector3(0, 0, RandomUtils.pointAt(transform.position, Vector3.zero));
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        GetComponent<Rigidbody2D>().linearVelocity = -transform.position.normalized;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable Damageable = collision.gameObject.GetComponent<IDamageable>();
        float damage = collision.relativeVelocity.magnitude;
        if (Damageable == null) return;
        Damageable.TakeDamage(damage);
       
    }
    
   
}
