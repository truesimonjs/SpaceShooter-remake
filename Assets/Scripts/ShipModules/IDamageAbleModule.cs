using UnityEngine;

public interface IDamageAbleModule
{
    float TryTakeDamage(float damage);
    int Priority { get; }
}
