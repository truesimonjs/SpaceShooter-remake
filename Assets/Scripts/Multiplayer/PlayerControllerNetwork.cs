using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerControllerNetwork : NetworkBehaviour
{
    //[SerializeField] private float playerSpeed = 3;
    //public static PlayerController instance;
    public ShipControllerRPC ship;

    public override void OnNetworkSpawn()
    {
        gameObject.name = GetComponent<NetworkObject>().OwnerClientId.ToString();
        enabled = IsOwner;
        if (ship == null) ship = GetComponent<ShipControllerRPC>();

    }
    void Update()
    {

        Camera.main.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y, Camera.main.transform.position.z);
        Camera.main.orthographicSize += -Input.GetAxis("Mouse ScrollWheel");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ship.PointShipRPC(mousePos);
    }


    private void FixedUpdate()
    {


        Vector2 playerMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        ship.MoveShipRPC(playerMove);
    }
    public override void OnNetworkDespawn()
    {
        if (IsOwner) 
        {
            ButtonManager.instance.spawnShipButton.SetActive(true);
        }
    }


}
