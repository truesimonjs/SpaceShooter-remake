using Unity.Netcode;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class ShipController : NetworkBehaviour
{
    [SerializeField] private float shipSpeed = 3;
    private Rigidbody2D rb;
    public bool useForces = true;
    public bool inControl = true;

    private void Start()
    {
        enabled = IsServer;
        rb = GetComponent<Rigidbody2D>();
    

       
    }
   
    [Rpc(SendTo.Server)]
    public void ServerUpdateRpc(Vector3 mousePos)
    {
        if (!useForces) transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * shipSpeed * Time.deltaTime;


        if (inControl) transform.eulerAngles = new Vector3(0, 0, RandomUtils.pointAt(transform.position, mousePos));
    }
   
    [Rpc(SendTo.Server)]
    public void AddForceRpc(Vector2 playerMove)
    {
        if (!useForces) return;
        if (inControl) rb.AddForce(playerMove * rb.mass * shipSpeed - rb.linearVelocity * rb.mass, ForceMode2D.Impulse);
        if (!inControl)
        {
            rb.AddForce(-rb.linearVelocity.normalized * rb.mass * shipSpeed);

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
