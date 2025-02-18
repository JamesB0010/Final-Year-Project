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


    [SerializeField] private float minRotation, maxRotation;

    private void Start()
    {
        int deviceIndex = this.device == eteeAPI.LeftDevice? 0: 1;
        eteeAPI.ResetControllerValues(deviceIndex);
    }


    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, (-Device.euler.z * this.rotationMultiplier) + this.rotationOffset, 0);
        this.ClampRotation();
    }

    private void ClampRotation()
    {
        float yRot = transform.eulerAngles.y;

        if (yRot > 290 || yRot < 200)
        {
            Debug.Log("Clamp");

            // Clamping the value to stay within [200, 290] degrees
            float clampedY = Mathf.Clamp(yRot, 200f, 290f);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, clampedY, transform.eulerAngles.z);
        }
    }
}
