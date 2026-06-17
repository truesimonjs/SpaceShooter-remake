using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float shipSpeed = 3;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void PointShip(Vector3 targetPos)
    {
        transform.eulerAngles = new Vector3(0, 0, RandomUtils.pointAt(transform.position, targetPos));
    }
    public virtual void MoveShip(Vector2 direction)
    {
       rb.linearVelocity = direction*shipSpeed;
    }
}

