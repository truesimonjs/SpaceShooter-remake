using UnityEngine;

public class ShipAi : MonoBehaviour
{
    private ShipController ship;
    private Vector3 lastKnownPos;
    [SerializeField]private float sightRange = 10;
    [SerializeField] private LayerMask layerMask;
    private void Awake()
    {
        ship = GetComponent<ShipController>();
        lastKnownPos = transform.position;
    }
    private void Update()
    {
        Transform targetT = CheckForTarget();
        if ( targetT != null)
        {
            lastKnownPos = targetT.position;
            Spotted();
        }
        else
        {
        ship.MoveTowards(lastKnownPos);

        }
            

        
    }
    protected virtual void Spotted()
    {
        ship.MoveTowards(lastKnownPos);
    }

    private Transform CheckForTarget()
    {
        Collider2D[] contacts = Physics2D.OverlapCircleAll(transform.position, sightRange, layerMask);
        if (contacts.Length > 0)
        {
         Collider2D other = contacts[0];
            Vector3 direction = (other.transform.position - transform.position).normalized;
         RaycastHit2D hit = Physics2D.Raycast(transform.position+direction, direction,Mathf.Infinity);
         if (hit && hit.collider == other) return other.transform;
        }

        return null;
    }

}
