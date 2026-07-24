using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ShipController ship;
    private Gun[] guns;
    private void Start()
    {
        if (ship == null) ship = GetComponent<ShipController>();
        guns = GetComponentsInChildren<Gun>();
        AudioListener.volume = 0f;
    }
    void Update()
    {

        Camera.main.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y, Camera.main.transform.position.z);
        Camera.main.orthographicSize += -Input.GetAxis("Mouse ScrollWheel");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ship.PointShip(mousePos);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            foreach (Gun gun in guns)
            {
                gun.Shoot(mousePos);
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioListener.volume = AudioListener.volume == 0 ? 0.25f : 0;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameObject go = InventoryMaster.instance.gameObject;
            go.SetActive(!go.activeSelf);
            
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("lshift presseed");
            GetComponentInChildren<ShipModuleMaster>().EngageModule("Boost");
        }
    }
    private void FixedUpdate()
    {


        Vector2 playerMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        ship.MoveShip(playerMove);
    }
    

}
