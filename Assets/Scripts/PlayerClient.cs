using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;
public class PlayerClient : NetworkBehaviour
{
    public static PlayerClient instance;
    public GameObject defaultShipPrefab;
    public ShipController shipController;
    public override void OnNetworkSpawn()
    {
        if (IsOwner) instance = this;
    }
    public void RequestShip()
    {
        SpawnShipRpc(OwnerClientId);
    }
    [Rpc(SendTo.Server)]
    public void SpawnShipRpc(ulong clientId)
    {
        GameObject shipObject = Instantiate(defaultShipPrefab);
        shipObject.GetComponent<NetworkObject>().Spawn();
        //NetworkObjectReference networkObjectReference = shipObject.GetComponent<NetworkObjectReference>();
        NetworkObjectReference networkObjectReference = new(shipObject);
        GetShipRpc(networkObjectReference, RpcTarget.Single(clientId, RpcTargetUse.Temp));
    }
    [Rpc(SendTo.SpecifiedInParams)]
    public void GetShipRpc(NetworkObjectReference networkObjectReference,RpcParams rpcParams)
    {

        networkObjectReference.TryGet(out NetworkObject networkObject);
        
        shipController = networkObject.GetComponent<ShipController>();
    }
}
