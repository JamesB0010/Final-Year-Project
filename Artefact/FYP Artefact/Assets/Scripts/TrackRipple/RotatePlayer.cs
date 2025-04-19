using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;


//Responsibilities
//
public class RotatePlayer : MonoBehaviour
{
    [SerializeField] private float rotationMultiplier;
    [SerializeField] private float rotationOffset;

    [FormerlySerializedAs("thingToRotate")] [SerializeField] private Transform boneToRotate;

    [SerializeField] private float minRotation, maxRotation;

    [SerializeField] private float softClampSpeed;

    [SerializeField] private float minimumInputMagnitude;

    private int deviceIndex = 0;

    private eteeDeviceHolder eteeDeviceHolder;

    private float offset;
    private void Start()
    {
        this.eteeDeviceHolder = GetComponentInParent<eteeDeviceHolder>();
        this.deviceIndex = this.eteeDeviceHolder.Device.isLeft ? 0 : 1;
        eteeAPI.ResetControllerValues(deviceIndex);
    }


    void Update()
    {
        bool playerAttemptingRecalibration = eteeAPI.GetIsPinchTrackpadGesture(this.deviceIndex);
        if (playerAttemptingRecalibration)
        {
            Recalibrate();
        }
        
        //rotate the player
        this.boneToRotate.localRotation = Quaternion.Euler( ((eteeDeviceHolder.Device.euler.z + offset) * this.rotationMultiplier) + this.rotationOffset,0, 0);
        
        this.ClampRotation();
    }

    public void Recalibrate()
    {
        offset = -this.eteeDeviceHolder.Device.euler.z;
        eteeAPI.ResetControllerValues(this.deviceIndex);
    }

    private void ClampRotation()
    {
        Vector3 startRotation = boneToRotate.localRotation.eulerAngles;
        float rotX = boneToRotate.localRotation.eulerAngles.x;

        bool remapRequired = (rotX > 180);
        float remappedRotX =  remapRequired ? rotX - 360 : rotX;

        if (remapRequired)
        {
            if (remappedRotX < this.minRotation)
            {
                float newRotationOffset = this.minRotation - remappedRotX;
                if (newRotationOffset > rotationOffset)
                    rotationOffset = Mathf.Lerp(rotationOffset, newRotationOffset, Time.deltaTime * this.softClampSpeed);
            }
            remappedRotX = Mathf.Max(remappedRotX, this.minRotation);
            boneToRotate.localRotation = Quaternion.Euler(remappedRotX + 360, startRotation.y, startRotation.z);
        }
        else
        {
            if (remappedRotX > this.maxRotation)
            {
                rotationOffset = Mathf.Lerp(rotationOffset, this.maxRotation - remappedRotX, Time.deltaTime * this.softClampSpeed);
            }
            remappedRotX = Mathf.Min(remappedRotX, this.maxRotation);
            boneToRotate.localRotation = Quaternion.Euler(remappedRotX, startRotation.y, startRotation.z);
        }
    }
}
