using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public partial class Fish 
{
    private class OverrideSteering
    {
        public bool active;
    
        public Vector3 overrideMoveToPosition;

        public float interestedTimeStamp;

        private UniTaskCompletionSource swimToBaitTCS = new UniTaskCompletionSource();

        public UniTaskCompletionSource SwimToBaitTCS => this.swimToBaitTCS;

        public void FishLostInterest()
        {
            this.active = false;
            this.SwimToBaitTCS.TrySetException(new Exception("Fish Lost Interest"));
        }

        public void FishInterestedInPoint(Vector3 location)
        {
            this.active = true;
            this.overrideMoveToPosition = location;
            this.interestedTimeStamp = Time.timeSinceLevelLoad;
        }
    }
}
