using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Arrowindicator : MonoBehaviour
{
    public Transform targetT;
    public Transform playerT;
    private void Update()
    {
        Vector3 direction = targetT.position - transform.localPosition;
        Vector3 limits = Camera.main.ViewportToWorldPoint(new Vector3(1,1,-10));

        transform.position = new Vector3(limits.x, limits.y, 0);
        float newX = Mathf.Abs(targetT.position.x) > limits.x ? limits.x : targetT.position.x;
        float newY = Mathf.Abs(targetT.position.y) > limits.y ? limits.y : targetT.position.y;
        transform.position = new Vector3 (newX, newY, 0);
    }

}
