using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ShipController : MonoBehaviour
{
    
    [SerializeField] public float shipSpeed = 3;
    [SerializeField] public float rotationSpeed = 360;
    [SerializeField] private Dictionary<string, ISkill> skills = new();
    private float targetRotation;
    private Vector2 targetVelocity = Vector2.zero;
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
    private void FixedUpdate()
    {

        if (knockbackTimer > Time.time) return;

        ShipRotationUpdate();
        ShipMovementUpdate();
    }
    private void ShipRotationUpdate()
    {
       
        float originRot = transform.eulerAngles.z;
        float result = Mathf.MoveTowardsAngle(originRot, targetRotation, rotationSpeed * Time.deltaTime);
        rb.rotation = result;
    }
    public float upSpeed = 5f;
    public float downSpeed = 1f;
    public float sidewardSpeed = 3f;
    private void ShipMovementUpdate()
    {
        Vector2 direction = targetVelocity;
        if (direction == Vector2.zero)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        direction = direction.normalized;
        Vector2 localDirection = transform.worldToLocalMatrix * direction;
        // max multiplier = what would you need to multiply the vector with to have that axis hit its top speed?
        float maxXMultiplier = sidewardSpeed / Mathf.Abs(localDirection.x);
        float maxYmultiplier = (localDirection.y > 0 ? upSpeed : downSpeed) / Mathf.Abs(localDirection.y);
        rb.linearVelocity = direction * Mathf.Min(maxXMultiplier, maxYmultiplier);
    }

    //
    public void ActivateSkill(string keyword)
    {
        skills[keyword]?.Activate();
    }
    public float targetRotDebug;
    public virtual bool PointShip(Vector3 targetPos)
    {
        const float rotationTolerance = 1;
        float originRot = transform.eulerAngles.z;
        targetRotation = RandomUtils.pointAt(transform.position, targetPos);
        float rotationDiff = (targetRotation-originRot);
        return Mathf.Abs(rotationDiff)<rotationTolerance;
    }
    public virtual void MoveShip(Vector2 direction)
    {
        
        if (direction.magnitude < 0.5f) targetVelocity = Vector2.zero;
        else targetVelocity = direction;
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
    private float knockbackTimer;
    public void TakeKnockback(Vector2 origin)
    {
        const float duration = 0.5f;
        knockbackTimer = Time.time + duration;
        Vector2 direction = (Vector2)transform.position - origin;
        rb.AddForce(direction, ForceMode2D.Impulse);

    }
}

