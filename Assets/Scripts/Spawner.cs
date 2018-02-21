using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float dis = 100;
    public GameObject enemyPrefab;
    public int sec = 15;
    public bool isSpawned = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetPosition = target.transform.position;
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance < dis && isSpawned == false)
        {
            StartCoroutine(Wait(sec));
        }
    }

    IEnumerator Wait(int seconds)
    {
        isSpawned = true;
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        Debug.Log("go");
        yield return new WaitForSeconds(seconds);
        isSpawned = false;
    }
}
