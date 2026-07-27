using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ShipController : MonoBehaviour
{
    
    [SerializeField] public float shipSpeed = 3;
    [SerializeField] public float rotationSpeed = 360;
    [SerializeField] private Dictionary<string, ISkill> skills = new();
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ISkill[] skillArr = GetComponentsInChildren<ISkill>();
        
        foreach (BoostSkill skill in skillArr)
        {
            skills.Add(skill.Activator, skill);
            skill.Initiate(this);
        }
    }
    public void ActivateSkill(string keyword)
    {
        skills[keyword]?.Activate();
    }
    public float targetRotDebug;
    public virtual bool PointShip(Vector3 targetPos)
    {
      
        float originRot = transform.eulerAngles.z;
        float targetRot = RandomUtils.pointAt(transform.position, targetPos);
        float result = Mathf.MoveTowardsAngle(originRot, targetRot, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, result);
        //rb.rotation = result;
        //targetRotDebug = targetRot;
        return targetRot == result;
    }
    public float upSpeed = 5f;
    public float downSpeed = 1f;
    public float sidewardSpeed = 3f;
    public virtual void MoveShip(Vector2 direction)
    {
        if (direction.magnitude < 0.5f) return;
        direction = direction.normalized;
        Vector2 localDirection = transform.worldToLocalMatrix * direction;
        // max multiplier: what would you need to multiply the vector with to have that axis hit its top speed?
         float maxXMultiplier = sidewardSpeed / Mathf.Abs(localDirection.x);
         float maxYmultiplier = (localDirection.y >0? upSpeed:downSpeed)/ Mathf.Abs(localDirection.y);
        
        //rb.linearVelocity = direction * Mathf.Min(maxXMultiplier, maxYmultiplier);
        transform.position += (Vector3)direction * Mathf.Min(maxXMultiplier, maxYmultiplier)*Time.deltaTime;
    }


    public virtual void MoveTowards(Vector2 destination, float distance = 0.25f)
    {
        Vector3 goal = destination - (Vector2)transform.position;
        if (goal.magnitude <= distance)
        {
            MoveShip(Vector2.zero);
        }
        else
        {
            MoveShip(goal.normalized);

        }
    }
}

