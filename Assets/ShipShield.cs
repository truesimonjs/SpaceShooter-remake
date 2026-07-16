using UnityEngine;

public class ShipShield : DamageAbleModule
{
    public float shields = 5;

    public override float TryTakeDamage(float damage = 1)
    {
        shields -= damage;
        if (shields > 0) 
        {
            damage = 0;
     
        } else
        {
            damage = -shields;
            shields = 0;
        }
        return damage;
    }
}
