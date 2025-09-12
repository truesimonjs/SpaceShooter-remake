using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
        //Instantiate(prefab,Random.insideUnitCircle*5,Quaternion.identity);
            
        float angle = Random.Range(0, 360);
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(prefab, quat*Vector3.right * 5, Quaternion.identity);
        }
    }
    
}
