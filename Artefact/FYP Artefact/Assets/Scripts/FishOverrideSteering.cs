using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public partial class Fish 
{
    public class OverrideSteering
    {
        public enum OverrideSteeringMode
        {
            toBait,
            possessed
        }

        public OverrideSteeringMode mode;
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
            this.mode = OverrideSteeringMode.toBait;
            this.overrideMoveToPosition = location;
        }

        public void PosessFish()
        {
            this.active = true;
            this.mode = OverrideSteeringMode.possessed;
        }
    }
}
