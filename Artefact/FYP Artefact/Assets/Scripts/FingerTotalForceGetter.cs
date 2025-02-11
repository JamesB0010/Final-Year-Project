using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTotalForceGetter : MonoBehaviour
{
    private eteeDevice[] devices;

    private void Start()
    {
        this.devices = new eteeDevice[2]
        {
            eteeAPI.LeftDevice, eteeAPI.RightDevice
        };
    }

    private float[] pullForces = new float[2] { 0, 0 };
    
    void Update()
    {
        for (int i = 0; i < this.devices.Length; i++)
        {
            this.pullForces[i] = this.GetTotalPullForce(this.devices[i]);
        }
    }

    private float GetTotalPullForce(eteeDevice device)
    {
        float playerForce = 0;
        var fingerForceData = device.fingerForceData;
        for (int i = 0; i < fingerForceData.Length; i++)
        {
            playerForce += fingerForceData[i];
        }

        return playerForce;
    }

    public float GetGenerousPullPercent(int deviceIndex)
    {
        float pullForce = this.pullForces[deviceIndex];
        float reducedPullForce = Mathf.Min(pullForce, 300f);
        reducedPullForce = Mathf.Max(0, reducedPullForce);

        float asPercent = reducedPullForce / 3f;
        return asPercent;
    }
}
