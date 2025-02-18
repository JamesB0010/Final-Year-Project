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

    private void Start()
    {
        int deviceIndex = this.device == eteeAPI.LeftDevice? 0: 1;
        eteeAPI.ResetControllerValues(deviceIndex);
    }


    void Update()
    {
        this.thingToRotate.localRotation = Quaternion.Euler( (Device.euler.z * this.rotationMultiplier) + this.rotationOffset,0, 0);
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
            remappedRotX = Mathf.Max(remappedRotX, this.minRotation);
            thingToRotate.localRotation = Quaternion.Euler(remappedRotX + 360, startRotation.y, startRotation.z);
            Debug.Log($"Remap required, current = {remappedRotX}");
        }
        else
        {
            remappedRotX = Mathf.Min(remappedRotX, this.maxRotation);
            thingToRotate.localRotation = Quaternion.Euler(remappedRotX, startRotation.y, startRotation.z);
            Debug.Log($"Remap not required, current = {remappedRotX}");
        }
        Debug.Log(remappedRotX);
    }
}
