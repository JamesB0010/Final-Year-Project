using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace eteeDeviceInput
{
    public class UiPlayerInput : MonoBehaviour
    {
        private TrackpadSwipe leftTrackpadEvents;
        public TrackpadSwipeUnityEvents leftEvents;
        
        private TrackpadSwipe rightTrackpadEvents;
        public TrackpadSwipeUnityEvents rightEvents;

        private void Start()
        {
            this.leftTrackpadEvents = new TrackpadSwipe(eteeAPI.LeftDevice);
            
            this.rightTrackpadEvents = new TrackpadSwipe(eteeAPI.RightDevice);
            
            this.BindInternalEvents();
        }

        private void BindInternalEvents()
        {
            this.leftTrackpadEvents.up += () => leftEvents.TrackpadSwipeUp?.Invoke();
            this.leftTrackpadEvents.down += () => leftEvents.TrackpadSwipeDown?.Invoke();

            this.rightTrackpadEvents.up += () => rightEvents.TrackpadSwipeUp?.Invoke();
            this.rightTrackpadEvents.down += () => rightEvents.TrackpadSwipeDown?.Invoke();
        }

        private void Update()
        {
            this.leftTrackpadEvents.Update();
            this.rightTrackpadEvents.Update();
        }
    }
}