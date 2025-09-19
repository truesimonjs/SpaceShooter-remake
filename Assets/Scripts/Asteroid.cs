using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour,IDamageable
{
    [SerializeField] private float health = 5;
    private void Start()
    {
        //transform.eulerAngles = new Vector3(0, 0, RandomUtils.pointAt(transform.position, Vector3.zero));
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        GetComponent<Rigidbody2D>().linearVelocity = -transform.position.normalized;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable Damageable = collision.gameObject.GetComponent<IDamageable>();
     
        if (Damageable == null) return;
        Damageable.TakeDamage();
      
    }
    public  void TakeDamage(float damage = 1)
    {
       
        health -= damage;
        StartCoroutine(DamageEffect());
        if (health <= 0) Destroy(gameObject,0.1f);


    }
    public IEnumerator DamageEffect()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }
   
}
