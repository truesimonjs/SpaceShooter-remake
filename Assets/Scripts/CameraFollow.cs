using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        if (PlayerController.instance == null) return;
        target = PlayerController.instance.transform;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        GetComponent<Camera>().orthographicSize += -Input.GetAxis("Mouse ScrollWheel");
    }
}
