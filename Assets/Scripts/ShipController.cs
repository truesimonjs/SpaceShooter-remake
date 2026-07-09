using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float shipSpeed = 3;
    [SerializeField] private float rotationSpeed = 5;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void PointShip(Vector3 targetPos)
    {
        float originRot = transform.eulerAngles.z;
        float targetRot = RandomUtils.pointAt(transform.position, targetPos);
        float result = Mathf.MoveTowardsAngle(originRot, targetRot, rotationSpeed*Time.deltaTime);
        
        transform.eulerAngles = new Vector3(0, 0, result);
        
    }
    public virtual void RotateShip(float targetRot)
    {
        float originRot = transform.eulerAngles.z;
        float result = Mathf.MoveTowardsAngle(originRot, targetRot, rotationSpeed * Time.deltaTime);

        transform.Rotate(Vector3.forward * result);
        
    }
    public virtual void MoveShip(Vector2 direction)
    {
        rb.linearVelocity = Vector2.zero;
        if (direction.magnitude < 0.5f) return;
       rb.linearVelocity = direction.normalized*shipSpeed;
    }
}

