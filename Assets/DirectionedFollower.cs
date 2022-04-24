using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionedFollower : FollowBehaviour
{
    public float desiredAngle = -90f;

    public override void SetTarget()
    {
        Vector3 relPos = target.position - transform.position;
        Vector3 middlePoint = relPos / 2;
        Vector3 leftedDirection = Quaternion.AngleAxis(desiredAngle, Vector3.up) * middlePoint;
        print(leftedDirection);

        agent.SetDestination(target.position + leftedDirection);
        transform.LookAt(target);
    }
}
