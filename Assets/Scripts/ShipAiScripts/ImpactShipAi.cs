using UnityEngine;

public class ImpactShipAi : ShipAi
{
    [SerializeField] private float boostDistance = 3f;


    protected override void Spotted()
    {
    
        bool pointedAtTarget = ship.PointShip(lastKnownPos);
        ship.MoveTowards(lastKnownPos, 0);
        bool withinBoostRange = Vector3.Distance(lastKnownPos, transform.position) < boostDistance;
        Debug.Log(pointedAtTarget ? "pointed at target" : "not pointed at target", this);
        if (pointedAtTarget && withinBoostRange)
        {
            //ship.ActivateSkill("Boost");
        }
    }
}
