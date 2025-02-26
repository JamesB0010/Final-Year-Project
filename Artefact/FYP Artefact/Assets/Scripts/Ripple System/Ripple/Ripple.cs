using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class Ripple : MonoBehaviour
{
    public Bounds wanderBounds;
    
    [SerializeField] private float speed;

    [SerializeField] private float timeBetweenNewWanderPos;

    private Vector3 wanderPos = new Vector3();

    private float lastGenerateWanderPosTimestamp = -90000;

    public void Initialize(Bounds wanderBounds) => this.wanderBounds = wanderBounds;

    private void Start() => RippleManager.RegisterNewRipple(this);

    private void Update()
    {
        TryGenerateNewWanderPos();

        MoveTowardsWanderPos();
    }
    private void TryGenerateNewWanderPos()
    {
        bool newWanderTimeDeltaSufficient = Time.timeSinceLevelLoad - lastGenerateWanderPosTimestamp >= timeBetweenNewWanderPos;
        if (newWanderTimeDeltaSufficient)
        {
            this.lastGenerateWanderPosTimestamp = Time.timeSinceLevelLoad;
            this.wanderPos = this.GenerateWanderPos();
        }
    }
    private Vector3 GenerateWanderPos() => this.wanderBounds.RandomXZInBounds();

    private void MoveTowardsWanderPos()
    {
        Vector3 direction = this.wanderPos - transform.position;
        direction.y = 0;
        direction.Normalize();

        Vector3 velocity = direction * (this.speed * Time.deltaTime);
        
        transform.Translate(velocity);
    }
}
