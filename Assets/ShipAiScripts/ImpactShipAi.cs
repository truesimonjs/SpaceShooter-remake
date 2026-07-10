using UnityEngine;

public class ImpactShipAi : ShipAi
{
    [SerializeField] private float boostDistance = 3f;
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
        if (Vector3.Distance(lastKnownPos, transform.position) < boostDistance)
        {
            ship.ActivateSkill("Boost");
        }
    }
}
