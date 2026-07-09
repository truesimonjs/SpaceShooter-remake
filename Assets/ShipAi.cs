using UnityEngine;

public class ShipAi : MonoBehaviour
{
    private ShipController ship;
    const float upDirAmount = 2;
    public Transform targetT;
    private void Awake()
    {
        ship = GetComponent<ShipController>();
    }
    const float sightRange = 1.0f;
    public bool LeftRaycast 
    { get
        {
            bool hit = Physics2D.Raycast(transform.position, transform.up*upDirAmount - transform.right, sightRange);
            Debug.Log(hit);
            return hit;
           
        } 
    }
    public bool RightRaycast
    {
        get
        {
            bool hit = Physics2D.Raycast(transform.position, transform.up*upDirAmount + transform.right, sightRange);
            Debug.Log(hit);
            return hit;

        }
    }

    private void Update()
    {
        if (LeftRaycast) ship.RotateShip(-5);
        else if (RightRaycast) ship.RotateShip(5);
        else ship.PointShip(targetT.position);
        ship.MoveShip(transform.up);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = LeftRaycast ? Color.red : Color.green;
        Gizmos.DrawRay(transform.position, (transform.up*upDirAmount - transform.right).normalized*sightRange);
        Gizmos.color = RightRaycast ? Color.red : Color.green;
        Gizmos.DrawRay(transform.position, (transform.up*upDirAmount + transform.right).normalized * sightRange);

    }
}
