using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun1 : MonoBehaviour
{
    
    public GameObject gooBall;
    public GameObject platform;
    public LineRenderer LRend;
    public GameObject laserStart;

    private Vector3 start;
    private Vector3 end;


    void FixedUpdate()
    {
        RaycastHit other;
        if (Physics.Raycast(transform.position, transform.forward, out other))
        {
            // shoot bullet
            if (Input.GetMouseButton(0) && other.transform.tag == "Enemy")
            {
                Instantiate(gooBall, other.point + new Vector3(0, 0.5f, -0.5f), Quaternion.identity);
                //print(other.transform.name);
            }

            // calculate distance between raycast hit point and player
            float distance = Vector3.Distance(other.transform.position, transform.position); 
            // build platform under conditions
            if (Input.GetMouseButtonDown(1) && distance > 2 && (other.transform.tag == "Environment" || other.transform.tag == "Platform"))
            {
                Instantiate(platform, other.point + new Vector3(0, 0.5f, 0), Quaternion.identity);
            }
            // update position of laser
            start = laserStart.transform.position;
            end = other.point;
        }
    }

    void Update()
    {
        // render laser between gun and raycast hit point
        LRend.SetPosition(0, start);
        LRend.SetPosition(1, end);
    }
}
