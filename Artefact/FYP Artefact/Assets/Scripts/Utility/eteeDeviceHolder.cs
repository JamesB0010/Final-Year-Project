using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eteeDeviceHolder : MonoBehaviour
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
}
