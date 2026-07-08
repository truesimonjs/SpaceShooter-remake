using UnityEngine;

public class ShipAi : MonoBehaviour
{
    private ShipController ship;
    private void Awake()
    {
        ship = GetComponent<ShipController>();
    }
    const float sightRange = 10.0f;
    public bool LeftRaycast 
    { get
        {
            bool hit = Physics2D.Raycast(transform.position, transform.up - transform.right, sightRange);
            Debug.Log(hit);
            return hit;
           
        } 
    }
    public bool RightRaycast
    {
        get
        {
            bool hit = Physics2D.Raycast(transform.position, transform.up + transform.right, sightRange);
            Debug.Log(hit);
            return hit;

        }
    }

    private void Update()
    {
        if (LeftRaycast) ship.RotateShip(-5);
        if (RightRaycast) ship.RotateShip(5);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = LeftRaycast ? Color.red : Color.green;
        Gizmos.DrawRay(transform.position, (transform.up - transform.right).normalized*sightRange);
        Gizmos.color = RightRaycast ? Color.red : Color.green;
        Gizmos.DrawRay(transform.position, (transform.up + transform.right).normalized * sightRange);

    }
}
