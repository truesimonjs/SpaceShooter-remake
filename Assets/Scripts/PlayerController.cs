using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : NetworkBehaviour
{
    //[SerializeField] private float playerSpeed = 3;
    //public static PlayerController instance;
    public ShipController ship;

    public override void OnNetworkSpawn()
    {
        gameObject.name = GetComponent<NetworkObject>().OwnerClientId.ToString();
        enabled = IsOwner;
        if (ship == null) ship = GetComponent<ShipController>();

    }
    void Update()
    {

        Camera.main.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y, Camera.main.transform.position.z);
        Camera.main.orthographicSize += -Input.GetAxis("Mouse ScrollWheel");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ship.PointShip(mousePos);
    }


    private void FixedUpdate()
    {


        Vector2 playerMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        ship.MoveShip(playerMove);
    }
    public override void OnNetworkDespawn()
    {
        if (IsOwner) 
        {
            ButtonManager.instance.spawnShipButton.SetActive(true);
        }
    }


}
