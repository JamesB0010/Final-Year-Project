using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class FingerTotalForceGetter
{
    public class DeviceHasPulledData
    {

        public DeviceHasPulledData(eteeDevice device)
        {
            this.device = device;
        }
        
        private eteeDevice device;
        private bool deviceHasPulled;
        public event Action deviceFirstPull;

        public void UpdateDeviceHasPulled(float pullForcePercent, eteeDevice device)
        {
            if (this.device != device)
                return;
            
            if (this.deviceHasPulled)
                return;

            if (pullForcePercent > 0)
            {
                this.deviceHasPulled = true;
                this.deviceFirstPull?.Invoke();
            }
        }
    }
    
    public static DeviceHasPulledData device1HasPulledData = new DeviceHasPulledData(eteeAPI.LeftDevice);
    
    public static DeviceHasPulledData device2HasPulledData = new DeviceHasPulledData(eteeAPI.RightDevice);
    private static float GetTotalPullForce(eteeDevice device) => device.fingerForceData.Sum();
    
    public static float GetGenerousPullPercent(eteeDevice device)
    {
        float pullForce = GetTotalPullForce(device);
        float reducedPullForce = Mathf.Min(pullForce, 300f);
        reducedPullForce = Mathf.Max(0, reducedPullForce);

        float asPercent = reducedPullForce / 3f;
        
        device1HasPulledData.UpdateDeviceHasPulled(asPercent, device);
        device2HasPulledData.UpdateDeviceHasPulled(asPercent, device);
        return asPercent;
    }
}
