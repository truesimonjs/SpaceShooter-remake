using UnityEngine;

public class ShipAi : MonoBehaviour
{
    protected ShipController ship;
    protected Vector3 lastKnownPos;
    [SerializeField] private float sightRange = 10;
    //[SerializeField] private LayerMask layerMask= 0;
    private void Awake()
    {
        ship = GetComponent<ShipController>();
        lastKnownPos = transform.position;
    }
    protected virtual void Update()
    {
        Transform targetT = CheckForTarget();
        if (targetT != null)
        {
            lastKnownPos = targetT.position;
            Spotted();
        }
        else
        {
            ship.PointShip(lastKnownPos);
            ship.MoveTowards(lastKnownPos);

        }
    }
    protected virtual void Spotted()
    {
        ship.PointShip(lastKnownPos);
        ship.MoveTowards(lastKnownPos);
    }

    protected Transform CheckForTarget()
    {
        LayerMask circleMask = 0;
        circleMask |= (1 << LayerMask.NameToLayer("Player"));
        Collider2D[] contacts = Physics2D.OverlapCircleAll(transform.position, sightRange, circleMask);
        if (contacts.Length > 0)
        {
            Collider2D other = contacts[0];
            Vector3 direction = (other.transform.position - transform.position).normalized;
            LayerMask rayMask = 0;
            rayMask |= (1 << LayerMask.NameToLayer("Obstacle"));
            rayMask |= (1 << LayerMask.NameToLayer("Player"));
            rayMask |= (1 << LayerMask.NameToLayer("Enemy"));
            RaycastHit2D hit = Physics2D.Raycast(transform.position + direction, direction, Mathf.Infinity, rayMask);
            if (hit && hit.collider == other) return other.transform;
        }

        return null;
    }

}
