using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnDistance = 5;
    [SerializeField] private float amount = 10;
    private void Start()
    {

        for (int i = 0; i < amount; i++)
        {
            //Instantiate(prefab,Random.insideUnitCircle*5,Quaternion.identity);

            float angle = Random.Range(0, 360);
            Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(prefab, transform.position+ quat * Vector3.up * spawnDistance, Quaternion.identity);

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,spawnDistance);
    }
}


