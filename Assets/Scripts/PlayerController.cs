using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 3;
    public static PlayerController instance;
    private Rigidbody2D rb;
    public bool useForce = true;
    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!useForce) rb.position += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized*Time.deltaTime*playerSpeed;
        //Debug.Log(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * Time.deltaTime * playerSpeed);
        
          //rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * playerSpeed;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.eulerAngles = new Vector3(0, 0, RandomUtils.pointAt(transform.position, mousePos));

    }
    private void FixedUpdate()
    {
        
        Vector2 playerMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        
      if (useForce)  rb.AddForce((playerMove-rb.linearVelocity.normalized) * rb.mass*playerSpeed,ForceMode2D.Impulse);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.enabled = false;
    }
}
