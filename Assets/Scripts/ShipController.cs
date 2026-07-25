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

    public virtual void PointShip(Vector3 targetPos)
    {
        float originRot = transform.eulerAngles.z;
        float targetRot = RandomUtils.pointAt(transform.position, targetPos);
        float result = Mathf.MoveTowardsAngle(originRot, targetRot, rotationSpeed * Time.deltaTime);

        //transform.eulerAngles = new Vector3(0, 0, result);

    }
    
    public virtual void RotateShip(float targetRot)
    {
        float originRot = transform.eulerAngles.z;
        float result = Mathf.MoveTowardsAngle(originRot, targetRot, rotationSpeed * Time.deltaTime);

        transform.Rotate(Vector3.forward * result);

    }
    public virtual void MoveShip(Vector2 direction)
    {
        rb.linearVelocity = Vector2.zero;
        if (direction.magnitude < 0.5f) return;
        direction = direction.normalized;
        MoveShipDirectionalSpeeds3(direction);
        return;
    
        rb.linearVelocity = direction.normalized * shipSpeed;
    }
    public float forwardMultiplier = 1.2f;
    public float backwardMultiplier = .5f;
    public float sidewardMultiplier = .7f;
    public float speedResult;
    public float yResultTest;
    public float xResultTest;
    public float speedMultiplierTest;
    public virtual void MoveShipDirectionalSpeeds(Vector2 direction)
    {
        
    
        direction = direction.normalized;
        //
        float dotResult = Vector3.Dot(direction, transform.up);
        Debug.Log(Vector3.Dot(transform.up, direction));
        float speedMultiplier = (dotResult >= 0) ? Mathf.Lerp(sidewardMultiplier, forwardMultiplier, dotResult) : Mathf.Lerp(sidewardMultiplier, backwardMultiplier, Mathf.Abs(dotResult));
        Vector2 movementVector = direction * speedMultiplier;
        speedResult = movementVector.magnitude;
        rb.linearVelocity = movementVector;
        yResultTest = movementVector.y;
        xResultTest = movementVector.x;
        speedMultiplierTest = speedMultiplier;
    
    }
    public virtual void MoveShipDirectionalSpeeds2(Vector2 direction)
    {
    
        
        Vector2 movementVector = new Vector2(direction.x*sidewardMultiplier, direction.y >0? direction.y*forwardMultiplier:direction.y*backwardMultiplier);
        Vector2 localDirection = transform.worldToLocalMatrix * direction;
        float xLimit = localDirection.x != 0 ? sidewardMultiplier : Mathf.Infinity;
        float yLimit;
        if (localDirection.y > 0) yLimit = forwardMultiplier;
        else if (localDirection.y == 0) yLimit = Mathf.Infinity;
        else yLimit = backwardMultiplier;
        float speedMultiplier = Mathf.Min(xLimit, yLimit);
        //rb.linearVelocity = transform.localToWorldMatrix* movementVector;
        rb.linearVelocity = direction * speedMultiplier;
        speedResult = rb.linearVelocity.magnitude;
        xResultTest = rb.linearVelocity.x;
        yResultTest = rb.linearVelocity.y;
    }
    public float maxXMultiplier;
    public float maxYmultiplier;
    public virtual void MoveShipDirectionalSpeeds3(Vector2 direction)
    {
        Vector2 localDirection = transform.worldToLocalMatrix * direction;
        //localDirection = localDirection.normalized;
        Debug.Log(localDirection.magnitude);
         maxXMultiplier = sidewardMultiplier / Mathf.Abs(localDirection.x);
         maxYmultiplier = (localDirection.y >0? forwardMultiplier:backwardMultiplier)/ Mathf.Abs(localDirection.y);
        
        rb.linearVelocity = direction *Mathf.Min(maxXMultiplier, maxYmultiplier);
        speedResult = rb.linearVelocity.magnitude;
        xResultTest = rb.linearVelocity.x;
        yResultTest = rb.linearVelocity.y;
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
            PointShip(destination);
            MoveShip(goal.normalized);

        }
    }
}

