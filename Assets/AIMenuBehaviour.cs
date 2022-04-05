using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMenuBehaviour : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    private Vector3 targetPosition;                                    // target to aim for

    public float xAmp;
    public float yAmp;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = true;
        agent.updatePosition = true;

        InvokeRepeating("ChangeTargetPosition", 0f, 5f);
    }

    private void Update()
    {
        if (targetPosition != null)
        {
            agent.SetDestination(targetPosition);
            //transform.LookAt(targetPosition);
        }
    }

    private void ChangeTargetPosition()
    {
        targetPosition = new Vector3(Random.Range(-xAmp, xAmp), 2.563184f, Random.Range(-yAmp, yAmp));
    }
}
