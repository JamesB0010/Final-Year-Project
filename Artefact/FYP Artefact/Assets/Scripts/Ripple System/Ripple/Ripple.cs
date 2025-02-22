using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    public Bounds wanderBounds;

    [SerializeField] private float timeBetweenNewWanderPos;

    private float lastGenerateWanderPosTimestamp = -90000;

    [SerializeField] private float speed;
    private Vector3 wanderPos = new Vector3();

    public void Initialize(Bounds wanderBounds)
    {
        this.wanderBounds = wanderBounds;
    }

    private void Update()
    {
        TryGenerateNewWanderPos();

        MoveTowardsWanderPos();
    }
    private void TryGenerateNewWanderPos()
    {
        if (Time.timeSinceLevelLoad - lastGenerateWanderPosTimestamp >= timeBetweenNewWanderPos)
        {
            this.lastGenerateWanderPosTimestamp = Time.timeSinceLevelLoad;
            this.wanderPos = this.GenerateWanderPos();
        }
    }
    private Vector3 GenerateWanderPos()
    {
        Vector3 wanderPos = this.wanderBounds.RandomXZInBounds();
        wanderPos.y = 0;
        return wanderPos;
    }

    private void MoveTowardsWanderPos()
    {
        Vector3 direction = this.wanderPos - transform.position;
        direction.y = 0;
        direction.Normalize();

        Vector3 velocity = direction * (this.speed * Time.deltaTime);
        
        transform.Translate(velocity);
    }
}
