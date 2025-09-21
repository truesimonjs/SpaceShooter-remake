using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float playerSpeed = 3;
    public static PlayerController instance;
    private Rigidbody2D rb;
    public bool useForces = true;
    public bool inControl = true;

    private void Start()
    {
        gameObject.name = GetComponent<NetworkObject>().OwnerClientId.ToString();
        rb = GetComponent<Rigidbody2D>();
        if(!IsOwner)
        {
     
            return;
        }
      
        instance = this;
    }
    void Update()
    {

       if (!IsOwner)  return; 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ServerUpdateRpc(mousePos);
    }
    [Rpc(SendTo.Server)]
    public void ServerUpdateRpc(Vector3 mousePos)
    {
        if (!useForces) transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * playerSpeed * Time.deltaTime;


        if (inControl) transform.eulerAngles = new Vector3(0, 0, RandomUtils.pointAt(transform.position, mousePos));
    }
    private void FixedUpdate()
    {
        if (!IsOwner) return;
        if (!useForces) return;
        Vector2 playerMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        AddForceRpc(playerMove);
          
      
   

    }
    [Rpc(SendTo.Server)]
    public void AddForceRpc(Vector2 playerMove)
    {
        if (inControl) rb.AddForce(playerMove * rb.mass * playerSpeed - rb.linearVelocity * rb.mass, ForceMode2D.Impulse);
        if (!inControl)
        {
            rb.AddForce(-rb.linearVelocity.normalized * rb.mass * playerSpeed);

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
