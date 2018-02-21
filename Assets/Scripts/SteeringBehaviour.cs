using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AIAgent))]
public class SteeringBehaviour : MonoBehaviour
{
    public float weighting = 8f;
    [HideInInspector]
    public AIAgent owner;

    protected virtual void Awake()
    {
        owner = GetComponent<AIAgent>();
    }

    public virtual Vector3 GetForce()
    {
        return Vector3.zero;
    }
}