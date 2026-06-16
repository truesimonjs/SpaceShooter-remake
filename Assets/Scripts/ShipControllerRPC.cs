using Unity.Netcode;
using UnityEngine;

public class ShipControllerRPC : ShipController
{
    [Rpc(SendTo.Server)]
    public virtual void PointShipRPC(Vector3 targetPos)
    {
        PointShip(targetPos);
    }
    [Rpc(SendTo.Server)]
    public void MoveShipRPC(Vector2 playerMove)
    {
        MoveShip(playerMove);
    }
}
