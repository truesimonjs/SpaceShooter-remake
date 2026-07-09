using UnityEngine;

public class ShooterShipAi : ShipAi
{
    [SerializeField] float shootDistance = 3f;
    [SerializeField] float stopDistance = 2.5f;
    private Gun[] guns;

    private void Start()
    {
        guns = GetComponentsInChildren<Gun>();
    }
    protected override void Spotted()
    {


        ship.MoveTowards(lastKnownPos, stopDistance);
        ship.PointShip(lastKnownPos);
        if (Vector3.Distance(lastKnownPos, transform.position) < shootDistance)
        {
            foreach (Gun gun in guns)
            {
                gun.Shoot(lastKnownPos);
            }
        }
    }
}
