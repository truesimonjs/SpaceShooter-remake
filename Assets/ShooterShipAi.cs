using UnityEngine;

public class ShooterShipAi : ShipAi
{
    [SerializeField] float shootDistance = 2.5f;
    protected override void Spotted()
    {
        //Vector3 direction = lastKnownPos - transform.position;
        //Vector3 goal = direction.normalized * (direction.magnitude - 5);
        ship.MoveTowards(lastKnownPos,shootDistance);
        ship.PointShip(lastKnownPos);
    }
}
