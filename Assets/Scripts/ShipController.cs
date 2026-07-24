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
        MoveShipDirectionalSpeeds(direction);
        return;
        rb.linearVelocity = Vector2.zero;
        if (direction.magnitude < 0.5f) return;
        rb.linearVelocity = direction.normalized * shipSpeed;
    }
    public float forwardMultiplier = 1.2f;
    public float backwardMultiplier = .5f;
    public float sidewardMultiplier = .7f;
    public float speedResult;
    public virtual void MoveShipDirectionalSpeeds(Vector2 direction)
    {
        direction = direction.normalized;
        rb.linearVelocity = Vector2.zero;
        if (direction.magnitude < 0.5f) return;
        //
        float dotResult = Vector3.Dot(direction, transform.up);
        Debug.Log(Vector3.Dot(transform.up, direction));
        float speedMultiplier = (dotResult >= 0) ? Mathf.Lerp(sidewardMultiplier, forwardMultiplier, dotResult) : Mathf.Lerp(sidewardMultiplier, backwardMultiplier, Mathf.Abs(dotResult));
        Vector2 movementVector = direction * speedMultiplier;
        speedResult = movementVector.magnitude;
        rb.linearVelocity = movementVector;

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

