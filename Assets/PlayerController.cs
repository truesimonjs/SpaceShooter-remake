using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 3;
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized*Time.deltaTime*playerSpeed;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.eulerAngles = new Vector3(0, 0, RandomUtils.pointAt(transform.position, mousePos));

    }
}
