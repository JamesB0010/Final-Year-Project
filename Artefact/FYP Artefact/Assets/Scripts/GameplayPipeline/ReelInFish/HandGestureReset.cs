using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGestureReset : MonoBehaviour
{
    [SerializeField] private HandGestureLocation[] handGestureLocations;

    public void ResetValues()
    {
        foreach (HandGestureLocation gestureLocation in this.handGestureLocations)
        {
            gestureLocation.ResetValues();
        }
    }
}
