using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public Transform target;                                    // target to aim for

    public float[] proximityDistance;
    public float[] followingSpeed;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    private void Update()
    {
        if (isAway())
        {
            return;
        }

        if (target != null)
        {
            int currentLevel = LevelManager.instance.currentLevel;
            agent.speed = followingSpeed[currentLevel];
            agent.SetDestination(target.position);
            transform.LookAt(target);
        }
    }

    public bool isAway()
    {
        Vector3 diff = target.position - transform.position;
        int currentLevel = LevelManager.instance.currentLevel;
        return diff.magnitude >= proximityDistance[currentLevel];
    }
}
