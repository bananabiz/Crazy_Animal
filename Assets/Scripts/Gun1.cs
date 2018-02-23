using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun1 : MonoBehaviour
{
    
    public GameObject gooBall;
    public GameObject platform;
    public GameObject laserStart;
    public float fireRate = .25f;
    public float weaponRange = 80f;
    //public float buildRange = 30f;
    public GameObject shotHint;

    private Vector3 start;
    private Vector3 end;
    private LineRenderer laserLine;
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.06f);
    private WaitForSeconds shotHintDuration = new WaitForSeconds(1.1f);
    private float nextFire;
    //private AudioSource gunAudio;


    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }
    
    void FixedUpdate()
    {
        RaycastHit other;
        if (Physics.Raycast(transform.position, transform.forward, out other))
        {
            // calculate distance between raycast hit point and player
            float distance = Vector3.Distance(other.transform.position, transform.position);

            // update position of laser
            start = laserStart.transform.position;
            end = other.point;

            // shoot bullet under shoot rate
            if (Input.GetButtonDown("Fire1") && other.transform.tag == "Enemy" && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                if (distance <= weaponRange)
                {
                    StartCoroutine(ShotEffect());
                    
                    Instantiate(gooBall, other.point + new Vector3(0, 0.5f, -0.5f), Quaternion.identity);
                    //print(other.transform.name);
                }
                if (distance > weaponRange)
                {
                    StartCoroutine(ShotHintEffect());
                }
            }
            
            // build platform in front of player under conditions
            if (Input.GetButtonDown("Fire2") && (other.transform.tag == "Environment" || other.transform.tag == "Platform"))
            {
                Instantiate(platform, transform.position + (transform.forward * 3), Quaternion.identity);
                
            }
            
        }
    }
    
    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();
        laserLine.enabled = true;

        // render laser between gun and raycast hit point
        laserLine.SetPosition(0, start);
        laserLine.SetPosition(1, end);

        yield return shotDuration;

        laserLine.enabled = false;
    }

    private IEnumerator ShotHintEffect()
    {
        shotHint.SetActive(true);
        yield return shotHintDuration;
        shotHint.SetActive(false);
    }
}
