using UnityEngine;

public class ImpactShipAi : ShipAi
{
    protected override void Update()
    {
        Transform targetT = CheckForTarget();
        if (targetT != null)
        {
            lastKnownPos = targetT.position;
            Spotted();
        }
        else
        {
            ship.MoveTowards(lastKnownPos);

        }
    }

    protected override void Spotted()
    {
        ship.PointShip(lastKnownPos);
        ship.MoveShip(transform.up);
    }
}
