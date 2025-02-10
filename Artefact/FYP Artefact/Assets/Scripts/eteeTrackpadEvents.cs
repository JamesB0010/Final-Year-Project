using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eteeTrackpadEvents : MonoBehaviour
{
    private eteeRemoteNavigationEvents leftTrackpadEvents;
    public eteeRemoteNavigationEvents LeftTrackpadEvents => this.leftTrackpadEvents;
    private eteeRemoteNavigationEvents rightTrackpadEvents;
    public eteeRemoteNavigationEvents RightTrackpadEvents => this.rightTrackpadEvents;

    private void Start()
    {
        this.leftTrackpadEvents = new eteeRemoteNavigationEvents(eteeAPI.LeftDevice);
        this.rightTrackpadEvents = new eteeRemoteNavigationEvents(eteeAPI.RightDevice);
    }

    private void Update()
    {
        this.leftTrackpadEvents.Update();
        this.rightTrackpadEvents.Update();
    }
}
