using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 5;

    public void TakeDamage(float damage = 1)
    {

        health -= damage;
        StartCoroutine(DamageEffect());
        if (health <= 0) Destroy(gameObject, 0.1f);


    }
    public IEnumerator DamageEffect()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }
}
