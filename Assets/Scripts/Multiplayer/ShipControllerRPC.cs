using Unity.Netcode;
using UnityEngine;

public class ShipControllerRPC : NetworkBehaviour
{
    private ShipController shipController;
    [Rpc(SendTo.Server)]
    public virtual void PointShipRPC(Vector3 targetPos)
    {
        shipController.PointShip(targetPos);
    }
    [Rpc(SendTo.Server)]
    public void MoveShipRPC(Vector2 playerMove)
    {
        shipController.MoveShip(playerMove);
    }
}
