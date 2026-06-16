using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ShipController ship;
    private void Start()
    {
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

}
