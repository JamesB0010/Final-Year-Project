using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AwakeEventExposer : MonoBehaviour
{
    [SerializeField] private UnityEvent AwakeEvent;

    private void Awake()
    {
        this.AwakeEvent?.Invoke();
    }
}
