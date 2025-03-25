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

        public float lostInterestTimeStamp = float.MinValue;

        private UniTaskCompletionSource swimToBaitTCS = new UniTaskCompletionSource();

        public UniTaskCompletionSource SwimToBaitTCS => this.swimToBaitTCS;

        public void FishLostInterest()
        {
            this.active = false;
            this.lostInterestTimeStamp = Time.timeSinceLevelLoad;
            this.SwimToBaitTCS.TrySetException(new Exception("Fish Lost Interest"));
            this.swimToBaitTCS = new UniTaskCompletionSource();
        }

        public void FishInterestedInPoint(Vector3 location)
        {
            this.active = true;
            this.overrideMoveToPosition = location;
        }
    }
}
