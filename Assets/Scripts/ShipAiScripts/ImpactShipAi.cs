using UnityEngine;

public class ImpactShipAi : ShipAi
{
    [SerializeField] private float boostDistance = 3f;


    protected override void Spotted()
    {
    
        bool pointedAtTarget = ship.PointShip(lastKnownPos);
        ship.MoveTowards(lastKnownPos, 0);
        bool withinBoostRange = Vector3.Distance(lastKnownPos, transform.position) < boostDistance;
        if (pointedAtTarget && withinBoostRange)
        {
            //ship.ActivateSkill("Boost");
        }
    }
}
