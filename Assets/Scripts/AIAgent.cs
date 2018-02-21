using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIAgent : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float maxDistance = 5f;
    public bool updatePosition = true;
    public bool updateRotation = true;

    [HideInInspector]
    public Vector3 velocity;

    private Vector3 force;
    private List<SteeringBehaviour> behaviours;
    private NavMeshAgent nav;

    // initialisation
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        behaviours = new List<SteeringBehaviour>(GetComponents<SteeringBehaviour>());
    }

    // calculates all forces from attached SteeringBehaviours
    void ComputeForces()
    {
        // reset force before calculation
        force = Vector3.zero;
        // loop through all behaviours
        for (int i = 0; i < behaviours.Count; i++)
        {
            // get current behaviours
            SteeringBehaviour b = behaviours[i];
            // check if behaviour is not active and enabled
            if (!b.isActiveAndEnabled)
            {
                // skip over to next behaviour
                continue;
            }
            // apply behaviour's force to our final force
            force += b.GetForce() * b.weighting;
            // check if force has gone over maxSpeed
            if (force.magnitude > maxSpeed)
            {
                // cap the force down to maxSpeed
                force = force.normalized * maxSpeed;
                // exit for loop
                break;
            }

        }
        
    }

    // applies the velocity to agent
    void ApplyVelocity()
    {
        // increase velocity by force
        velocity += force * Time.deltaTime;
        // update nav's speed to velocity
        nav.speed = velocity.magnitude;
        // is there a velocity?
        if (velocity.magnitude > 0)
        {
            // is the velocity over maxSpeed?
            if (velocity.magnitude > maxSpeed)
            {
                // cap velocity to maxSpeed
                velocity = velocity.normalized * maxSpeed;
            }
            // predict the next position
            Vector3 nextPos = transform.position + velocity;
            // perform NavMesh Sampling
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(nextPos, out navHit, maxDistance, -1))
            {
                // set nav destination to nav hit position
                nav.SetDestination(navHit.position);
            }
        }
    }

    void Update()
    {
        nav.updatePosition = updatePosition;
        nav.updateRotation = updateRotation;
        ComputeForces();
        ApplyVelocity();
    }
}