using UnityEngine;

public abstract class DamageAbleModule : MonoBehaviour
{
    public abstract float TryTakeDamage(float damage = 1);
    public int priority = 0;
}
