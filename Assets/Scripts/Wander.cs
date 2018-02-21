using UnityEngine;
using System.Collections;


public class Wander : SteeringBehaviour
{
    public float offset = 1f;
    public float radius = 1f;
    public float jitter = 0.2f;

    private Vector3 targetDir;
    private Vector3 randomDir;

    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;  //set force to zero
    
        float randX = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);  //float's max value
        float randZ = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);

        bool inputDown = false;

        if (inputDown)
        {
            print("I pressed somethin'!");
        }

        #region Calculate Random Direction
        randomDir = new Vector3(randX, 0, randZ);  // Create the random direction vector
        randomDir = randomDir.normalized;  // Normalize the random direction
        //same with randomDir.Normalize(); // Normalize is 1 smallest unit of moving towards endpoint
        randomDir *= jitter;  // Multiply randomDir by jitter
        #endregion

        #region Calculate Target Direction
        targetDir += randomDir;  // Append target direction with random directon
        targetDir = targetDir.normalized;  // Normalize the target direction
        targetDir *= radius;  // Multiply target direction by the radius
        #endregion

        // Calculate seek position using targetDir
        Vector3 seekPos = transform.position + targetDir;
        seekPos += transform.forward * offset;
    
        #region Wander
        // Calculate direction
        Vector3 direction = seekPos - transform.position;
        // Is direction valid? (not zero)
        if (direction.magnitude > 0)
        {
            // Calculate force
            Vector3 desiredForce = direction.normalized * weighting;
            force = desiredForce - owner.velocity;
        }

        #endregion

        return force;  // Return the force
    }
}