using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public partial class Fish 
{
    public class OverrideSteering
    {
        public bool active;
    
        public Vector3 overrideMoveToPosition;

        public float lostInterestTimeStamp = float.MinValue;

        public event Action lostInterestInBait;


        public void FishLostInterest()
        {
            this.active = false;
            this.lostInterestTimeStamp = Time.timeSinceLevelLoad;
            this.lostInterestInBait?.Invoke();
        }

        public void FishInterestedInPoint(Vector3 location)
        {
            this.active = true;
            this.overrideMoveToPosition = location;
        }
    }
}
