using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private FishSteeringBehaviour[] fishSteeringBehvaiours;
    private Rigidbody rigidBody;

    [SerializeField] private float maxVelocity;
    
    private void Awake()
    {
        this.fishSteeringBehvaiours = GetComponents<FishSteeringBehaviour>();
        this.rigidBody = GetComponent<Rigidbody>();
        this.rigidBody.velocity = transform.forward * maxVelocity;
    }

    private void LateUpdate()
    {
        this.rigidBody.velocity = this.SumFishSteeringBehaviourVelocities();
    }

    private Vector3 SumFishSteeringBehaviourVelocities()
    {
        Vector3 sum = Vector3.zero;

        foreach (FishSteeringBehaviour fishSteeringBehvaiour in this.fishSteeringBehvaiours)
        {
            sum += fishSteeringBehvaiour.velocity;
        }

        return sum.normalized * Mathf.Clamp(sum.magnitude, 0, this.maxVelocity);
    }
}
