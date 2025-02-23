using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField] private float rotationMultiplier;
    [SerializeField] private float rotationOffset;

    [SerializeField] private Transform thingToRotate;

    [SerializeField] private float minRotation, maxRotation;

    [SerializeField] private float softClampSpeed;

    [SerializeField] private float minimumInputMagnitude;

    private int deviceIndex = 0;

    private eteeDeviceHolder eteeDeviceHolder;

    private void Start()
    {
        this.eteeDeviceHolder = GetComponent<eteeDeviceHolder>();
        this.deviceIndex = this.eteeDeviceHolder.Device == eteeAPI.LeftDevice? 0: 1;
        this.deviceIndex = this.eteeDeviceHolder.Device.isLeft ? 0 : 1;
        eteeAPI.ResetControllerValues(deviceIndex);
    }

    private float offset;

    void Update()
    {
        if (eteeAPI.GetIsPinchTrackpadGesture(this.deviceIndex))
        {
            Debug.Log("Pinch");
            offset = -this.eteeDeviceHolder.Device.euler.z;
            eteeAPI.ResetControllerValues(this.deviceIndex);
        }
        
        this.thingToRotate.localRotation = Quaternion.Euler( ((eteeDeviceHolder.Device.euler.z + offset) * this.rotationMultiplier) + this.rotationOffset,0, 0);
        
        this.ClampRotation();
    }

    private void ClampRotation()
    {
        Vector3 startRotation = thingToRotate.localRotation.eulerAngles;
        float rotX = thingToRotate.localRotation.eulerAngles.x;

        bool remapRequired = (rotX > 180);
        float remappedRotX =  remapRequired ? rotX - 360 : rotX;

        if (remapRequired)
        {
            if (remappedRotX < this.minRotation)
            {
                float newRotationOffset = this.minRotation - remappedRotX;
                if (newRotationOffset > rotationOffset)
                    rotationOffset = Mathf.Lerp(rotationOffset, newRotationOffset, Time.deltaTime * this.softClampSpeed);
                //Debug.Log($"Difference is {this.minRotation - remappedRotX}");
            }
            remappedRotX = Mathf.Max(remappedRotX, this.minRotation);
            thingToRotate.localRotation = Quaternion.Euler(remappedRotX + 360, startRotation.y, startRotation.z);
        }
        else
        {
            if (remappedRotX > this.maxRotation)
            {
                rotationOffset = Mathf.Lerp(rotationOffset, this.maxRotation - remappedRotX, Time.deltaTime * this.softClampSpeed);
            }
            remappedRotX = Mathf.Min(remappedRotX, this.maxRotation);
            thingToRotate.localRotation = Quaternion.Euler(remappedRotX, startRotation.y, startRotation.z);
        }
        //Debug.Log(remappedRotX);
    }
}
