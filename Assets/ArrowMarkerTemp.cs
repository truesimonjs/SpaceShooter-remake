using UnityEngine;

public class ArrowMarkerTemp : MonoBehaviour
{
    public GameObject arrowPointer;
    private void Start()
    {
        Instantiate(arrowPointer).GetComponent<Arrowindicator>().targetT = transform;

    }
}
