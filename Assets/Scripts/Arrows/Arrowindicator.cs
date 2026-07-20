using UnityEngine;

public class Arrowindicator : MonoBehaviour
{
    public Transform targetT;
    //public Transform playerT;
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
     
        if (targetT == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 limitsMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 limitsMin = Camera.main.ViewportToWorldPoint(Vector3.zero);

        float sizeAdjust = transform.localScale.x / 2;


        float newX = Mathf.Clamp(targetT.position.x, limitsMin.x + sizeAdjust, limitsMax.x - sizeAdjust);
        float newY = Mathf.Clamp(targetT.position.y, limitsMin.y + sizeAdjust, limitsMax.y - sizeAdjust);
        bool onScreen = newX == targetT.position.x && newY == targetT.position.y;
        GetComponentInChildren<SpriteRenderer>().enabled = targetT.gameObject.activeInHierarchy && !onScreen;
        transform.position = new Vector3(newX, newY, 0);
    }

}
