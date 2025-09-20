using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 3;
    public static PlayerController instance;
    private Rigidbody2D rb;
    public bool useForces = true;
    public bool inControl = true;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!useForces) transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0).normalized * playerSpeed * Time.deltaTime;
        
        
       if (inControl) transform.eulerAngles = new Vector3(0, 0, RandomUtils.pointAt(transform.position, mousePos));
    }
    private void FixedUpdate()
    {
        
        Vector2 playerMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if (!useForces) return;

          
        if (inControl)rb.AddForce(playerMove * rb.mass * playerSpeed-rb.linearVelocity*rb.mass, ForceMode2D.Impulse);
        if (!inControl) 
        { 
            rb.AddForce(-rb.linearVelocity.normalized * rb.mass * playerSpeed);
            
            inControl = rb.linearVelocity.magnitude < 0.1f;
            if (inControl) rb.angularVelocity = 0;
        }
   

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //rb.AddTorque(1*Mathf.Deg2Rad  ,ForceMode2D.Impulse);
        inControl = false; 
    }
}
