using UnityEngine;

public class ImpactShipAi : MonoBehaviour
{
    private Transform playerT;
    private ShipController ship;
    private void Awake()
    {
        ship = GetComponent<ShipController>();
    }
    private void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        ship.PointShip(playerT.position);
        ship.MoveShip(playerT.position-transform.position);
    }
}
