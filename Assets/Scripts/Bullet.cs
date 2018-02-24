﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ring;
    public float destroyDelay = 1.5f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == ("Enemy"))
        {
            //attach bullet on enemy
            transform.parent = other.gameObject.transform;
            Destroy(this.gameObject, destroyDelay - 0.2f);
            //instantiate portal to capture enemy
            GameObject ringClone = Instantiate(ring, other.transform.position, Quaternion.identity);
            Destroy(ringClone, destroyDelay);
            Destroy(other.transform.parent.gameObject, destroyDelay);
            Destroy(other.gameObject, destroyDelay);
        }
    } 
}