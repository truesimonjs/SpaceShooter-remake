using UnityEngine;

public class ShipAi : MonoBehaviour
{
    private ShipController ship;
    public Transform targetT;
    private void Awake()
    {
        ship = GetComponent<ShipController>();
    }
  
}
