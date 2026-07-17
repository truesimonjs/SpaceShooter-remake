using UnityEngine;

public class GeneratorModule : MonoBehaviour
{
    private ShipModuleMaster master;
    [SerializeField] private float energyRate;
    float timer = 0;
    private void Start()
    {
        master = GetComponent<ShipModuleMaster>();
    }

    private void Update()
    {
        const float cd = 1;
        timer -= Time.deltaTime;
        if (timer <= 0) 
        {
            master.ChangeEnergyBool(energyRate);
            timer = cd;
        }
    }
}
