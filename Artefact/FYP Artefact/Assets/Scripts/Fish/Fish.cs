using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public partial class Fish : MonoBehaviour
{
    public FishTypes fishType;
    private FishSteeringBehaviour[] fishSteeringBehvaiours;
    private Rigidbody rigidBody;
    
    [SerializeField] private Transform mouth;

    [SerializeField] private float maxVelocity;
    [SerializeField] private float zRotSpeed;
    [SerializeField] private float GeneralRotationSpeed;

    [SerializeField] private float stopDistanceForTakingBait;


    public OverrideSteering overrideSteeringSettings = new OverrideSteering();

    private void Awake()
    {
        this.fishSteeringBehvaiours = GetComponents<FishSteeringBehaviour>();
        this.rigidBody = GetComponent<Rigidbody>();
        this.rigidBody.velocity = transform.forward * maxVelocity;
    }

    private void LateUpdate()
    {
        if (overrideSteeringSettings.active && this.overrideSteeringSettings.mode == OverrideSteering.OverrideSteeringMode.toBait)
        {
            this.rigidBody.velocity = this.overrideSteeringSettings.overrideMoveToPosition - mouth.position;
            if (this.rigidBody.velocity.magnitude > this.maxVelocity)
                this.rigidBody.velocity = this.rigidBody.velocity.normalized * this.maxVelocity;

            if ((this.overrideSteeringSettings.overrideMoveToPosition - mouth.position).magnitude <= this.stopDistanceForTakingBait)
                this.overrideSteeringSettings.FishLostInterest();
        }
        else if(overrideSteeringSettings.active == false)
        {
            this.rigidBody.velocity = this.SumFishSteeringBehaviourVelocities();
        }

        if (this.rigidBody.velocity == Vector3.zero)
            return;

        Quaternion oldRot = transform.rotation;
        
        transform.forward = this.rigidBody.velocity;
        Vector3 rot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rot.x, rot.y, 0), Time.deltaTime * this.zRotSpeed);

        transform.rotation = Quaternion.Lerp(oldRot, transform.rotation, Time.deltaTime * this.GeneralRotationSpeed);
    }

    private Vector3 SumFishSteeringBehaviourVelocities()
    {
        Vector3 sum = Vector3.zero;

        foreach (FishSteeringBehaviour fishSteeringBehaviour in this.fishSteeringBehvaiours)
        {
            sum += fishSteeringBehaviour.velocity;
        }

        return sum.normalized * Mathf.Clamp(sum.magnitude, 0, this.maxVelocity);
    }

    public OverrideSteering OverideSteeringTowards(Vector3 location)
    {
        this.overrideSteeringSettings.FishInterestedInPoint(location);
        return this.overrideSteeringSettings;
    }

    public bool HasLostInterestForLongEnough()
    {
        float timeSinceLostInterest = Time.timeSinceLevelLoad - this.overrideSteeringSettings.lostInterestTimeStamp;
        return  timeSinceLostInterest >= 7;
    }

    public void PosessFish()
    {
        this.overrideSteeringSettings.PosessFish();
        this.rigidBody.velocity = Vector3.zero;
        this.rigidBody.isKinematic = true;
    }
}
