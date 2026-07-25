using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BoostSkill : MonoBehaviour, ISkill
{
    [SerializeField] private float speed = 6;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] float cooldown = 1f;
    [field:SerializeField]public string Activator { get; set; } = "Boost";
    
    ShipController ship;
    private float nextActivate = 0f;
    private float oldSpeed;
    private float oldRotationSpeed;
  
    public void Initiate (ShipController _ship)
    {
        ship = _ship;
        oldSpeed = ship.upSpeed;
        oldRotationSpeed = ship.rotationSpeed;
    }
    public void Activate()
    {
        StartCoroutine(Boost());

    }
    private IEnumerator Boost()
    {
        if (Time.time >= nextActivate) 
        {
            ship.upSpeed = speed;
            ship.rotationSpeed = rotationSpeed;
            yield return new WaitForSeconds(duration);
            ship.upSpeed = oldSpeed;
            ship.rotationSpeed = oldRotationSpeed;
            nextActivate = Time.time + cooldown;
        }
     
    }
}
