using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    // Use this for initialization
    void Start ()
    {
        maxHealth = 100;
        //reset health to full on game load
        currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
        
	}

    //void OnCollisionEnter(Collider enemy)
    //{
    //    if (enemy.gameObject.tag == "Enemy")
    //    {
    //        currentHealth -= enemy.attackDamage;
    //    }
    //}
}
