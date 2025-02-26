using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class FingerTotalForceGetter 
{
    private static float GetTotalPullForce(eteeDevice device) => device.fingerForceData.Sum();
    
    public static float GetGenerousPullPercent(eteeDevice device)
    {
        float pullForce = GetTotalPullForce(device);
        float reducedPullForce = Mathf.Min(pullForce, 300f);
        reducedPullForce = Mathf.Max(0, reducedPullForce);

        float asPercent = reducedPullForce / 3f;
        return asPercent;
    }
}
