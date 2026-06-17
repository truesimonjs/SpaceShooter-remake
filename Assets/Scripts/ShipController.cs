using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float shipSpeed = 3;
    private Rigidbody2D rb;
    public bool useForces = true;
    public bool inControl = true;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();



    }

    public virtual void PointShip(Vector3 targetPos)
    {
        if (inControl) transform.eulerAngles = new Vector3(0, 0, RandomUtils.pointAt(transform.position, targetPos));
    }
    public virtual void MoveShip(Vector2 direction)
    {
        if (!useForces)
        {
            transform.position += (Vector3)direction * shipSpeed * Time.deltaTime;
            return;
        }
        if (inControl) rb.AddForce(direction.normalized * rb.mass * shipSpeed - rb.linearVelocity * rb.mass, ForceMode2D.Impulse); //push ship by whatever amount
        if (!inControl)
        {
            rb.AddForce(-rb.linearVelocity.normalized * rb.mass * shipSpeed); //add normalized force counteracting the current spin, once the spin has been slowed control is regained

            inControl = rb.linearVelocity.magnitude < 0.1f;
            if (inControl) rb.angularVelocity = 0;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + " was hit!");
        //rb.AddTorque(1*Mathf.Deg2Rad  ,ForceMode2D.Impulse);
        inControl = false;
    }
}
