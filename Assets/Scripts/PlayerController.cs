using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : NetworkBehaviour
{
    //[SerializeField] private float playerSpeed = 3;
    //public static PlayerController instance;
    public ShipController shipController;


    public override void OnNetworkSpawn()
    {
        gameObject.name = GetComponent<NetworkObject>().OwnerClientId.ToString();
        enabled = IsOwner;
        if(shipController ==null) shipController = GetComponent<ShipController>();
      
    }
    void Update()
    {
       
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        Camera.main.orthographicSize += -Input.GetAxis("Mouse ScrollWheel");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shipController.ServerUpdateRpc(mousePos);
    }
   
   
    private void FixedUpdate()
    {
        
        
        Vector2 playerMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        shipController.AddForceRpc(playerMove);
          
      
   

    }
    
 
 
}
