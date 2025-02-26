using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utility
{
    public class AwakeEventExposer : MonoBehaviour
    {
        [SerializeField] private UnityEvent AwakeEvent;

        private void Awake()
        {
            this.AwakeEvent?.Invoke();
        }
    }
}