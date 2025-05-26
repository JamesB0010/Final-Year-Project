using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float currentTime;

    private int lastInt;

    public UnityEvent<int> IntTick;

    public UnityEvent TimeUp;


    private void Awake()
    {
        this.lastInt = (int)currentTime;
    }

    private void Update()
    {
        this.currentTime -= Time.deltaTime;

        var intTime = (int)this.currentTime;
        if (intTime != this.lastInt)
        {
            this.lastInt = intTime;
            this.IntTick?.Invoke(this.lastInt);
        }

        if (this.currentTime <= 0)
        {
            Debug.Log("Time up");
            this.TimeUp?.Invoke();
            this.enabled = false;
        }
    }
}