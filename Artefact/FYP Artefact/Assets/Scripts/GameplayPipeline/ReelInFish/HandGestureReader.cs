using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGestureReader : MonoBehaviour
{
    [SerializeField] private HandGestureProfile closed;
    [SerializeField] private HandGestureProfile open;
    [SerializeField] private HandGestureProfile rockOn;
    
    private eteeDeviceHolder eteeDeviceHolder;
    
    private void Awake()
    {
        this.eteeDeviceHolder = GetComponentInParent<eteeDeviceHolder>();
    }

    public HandGesture CollapseToHandGesture()
    {
        float indexPull = this.eteeDeviceHolder.Device.fingerPullData[1];
        float middlePull = this.eteeDeviceHolder.Device.fingerPullData[2];
        float ringPull = this.eteeDeviceHolder.Device.fingerPullData[3];
        float pinkyPull = this.eteeDeviceHolder.Device.fingerPullData[4];

        float closedLikeness = this.closed.GetPoseLikeness(indexPull, middlePull, ringPull, pinkyPull);
        float openLikeness = this.open.GetPoseLikeness(indexPull, middlePull, ringPull, pinkyPull);
        float rockOnLikeness = this.rockOn.GetPoseLikeness(indexPull, middlePull, ringPull, pinkyPull);

        bool handClosed = closedLikeness > openLikeness && closedLikeness > rockOnLikeness;
        bool handOpen = openLikeness > closedLikeness && openLikeness > rockOnLikeness;
        bool handRockOn = rockOnLikeness > closedLikeness && rockOnLikeness > openLikeness;
        
        Debug.Log($"index: {indexPull}, middle: {middlePull}, ring: {ringPull}, pinky: {pinkyPull}");
        
        if (handClosed)
        {
            Debug.Log("Hand Closed");
            return HandGesture.closed;
        }
        else if (handOpen)
        {
            Debug.Log("Hand Open");
            return HandGesture.open;
        }
        else if (handRockOn)
        {
            Debug.Log("Hand Rock On");
            return HandGesture.rockOn;
        }

        return HandGesture.err;
    }

    private void Update()
    {
       this.CollapseToHandGesture(); 
    }
}
