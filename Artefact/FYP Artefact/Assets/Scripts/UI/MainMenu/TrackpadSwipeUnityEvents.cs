using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace eteeDeviceInput
{
    [Serializable]
    public class TrackpadSwipeUnityEvents
    {
        public UnityEvent TrackpadSwipeUp;
        public UnityEvent TrackpadSwipeDown;
        public UnityEvent TrackpadSwipeLeft;
        public UnityEvent TrackpadSwipeRight;
    }
}