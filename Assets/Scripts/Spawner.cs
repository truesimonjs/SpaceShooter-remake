using Unity.Netcode;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    [SerializeField] private GameObject prefab;
    private void Start()
    {
        if (!IsServer) return;
        for (int i = 0; i < 10; i++)
        {
            //Instantiate(prefab,Random.insideUnitCircle*5,Quaternion.identity);

            float angle = Random.Range(0, 360);
            Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(prefab, quat * Vector3.up * 5, Quaternion.identity);

        }
    }
        public override void OnNetworkSpawn()
        {
            if (!IsServer||!enabled) return;
            
            for (int i = 0; i < 10; i++)
            {
                //Instantiate(prefab,Random.insideUnitCircle*5,Quaternion.identity);

                float angle = Random.Range(0, 360);
                Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
               GameObject spawnee = Instantiate(prefab, quat * Vector3.up * 5, Quaternion.identity);
            
          
            spawnee.GetComponent<NetworkObject>().Spawn();
            }
    }
    
   
    
}
