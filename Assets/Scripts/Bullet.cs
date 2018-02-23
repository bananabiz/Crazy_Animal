using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ring;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == ("Enemy"))
        {
            Destroy(this.gameObject, 1f);

            GameObject ringClone = Instantiate(ring, other.transform.position, Quaternion.identity);
            Destroy(ringClone, 1.5f);
            Destroy(other.transform.parent.gameObject, 1.5f);
            Destroy(other.gameObject, 1.5f);
        }
    } 
}