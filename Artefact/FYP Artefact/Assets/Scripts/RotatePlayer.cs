using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private eteeDevice device;
    public eteeDevice Device
    {
        get => this.device;
        set
        {
            this.device = value;
            this.device.CalibrateGyro();
        }
    }
    
    [SerializeField] private float rotationMultiplier;
    [SerializeField] private float rotationOffset;

    [SerializeField] private Transform thingToRotate;

    [SerializeField] private float minRotation, maxRotation;

    [SerializeField] private float softClampSpeed;

    private void Start()
    {
        int deviceIndex = this.device == eteeAPI.LeftDevice? 0: 1;
        eteeAPI.ResetControllerValues(deviceIndex);
    }


    private Vector3 lastDeviceEuler = new Vector3();
    void Update()
    {
        float delta = (device.euler - lastDeviceEuler).magnitude;
        Debug.Log((device.euler - lastDeviceEuler).magnitude);
        lastDeviceEuler = device.euler;
        if (delta > 0.1)
        { 
            this.thingToRotate.localRotation = Quaternion.Euler( (Device.euler.z * this.rotationMultiplier) + this.rotationOffset,0, 0);
        }
        
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
                    rotationOffset = newRotationOffset;
                //Debug.Log($"Difference is {this.minRotation - remappedRotX}");
            }
            remappedRotX = Mathf.Max(remappedRotX, this.minRotation);
            thingToRotate.localRotation = Quaternion.Euler(remappedRotX + 360, startRotation.y, startRotation.z);
        }
        else
        {
            if (remappedRotX > this.maxRotation)
            {
                rotationOffset = this.maxRotation - remappedRotX;
            }
            remappedRotX = Mathf.Min(remappedRotX, this.maxRotation);
            thingToRotate.localRotation = Quaternion.Euler(remappedRotX, startRotation.y, startRotation.z);
        }
        //Debug.Log(remappedRotX);
    }
}
