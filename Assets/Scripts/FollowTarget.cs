using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    public int dis = 50;
    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // update player's position
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetPosition = target.transform.position;
        float distance = Vector3.Distance(transform.position, targetPosition);

        // move enemy towards player if player close enough
        if (distance < dis)
        {
            
            transform.LookAt(target.transform);
            agent.SetDestination(targetPosition);
        }
    }
}
