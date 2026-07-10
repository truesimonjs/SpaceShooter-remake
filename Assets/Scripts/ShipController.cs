using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    
    [SerializeField] public float shipSpeed = 3;
    [SerializeField] public float rotationSpeed = 360;
    [SerializeField] private Dictionary<string, BoostSkill> skills = new();
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BoostSkill[] skillArr = GetComponentsInChildren<BoostSkill>();
        
        foreach (BoostSkill skill in skillArr)
        {
            skills.Add(skill.activator, skill);
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

        transform.eulerAngles = new Vector3(0, 0, result);

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
        rb.linearVelocity = direction.normalized * shipSpeed;
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

