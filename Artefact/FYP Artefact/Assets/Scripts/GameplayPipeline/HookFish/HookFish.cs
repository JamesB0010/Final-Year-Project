using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class HookFish : MonoBehaviour
{
    private List<float> armRaiseValueBuffer = new List<float>();

    private int armRaiseValueBufferPointer = 0;

    [SerializeField] private float minFlickDeviation;

    private float armRaiseBufferStartTimestamp = 0;

    [SerializeField] private float armBufferTimeLength;

    [SerializeField] private int playerIndex;

    [SerializeField] private Transform fishHook;

    [SerializeField] private UnityEvent hookedFish;

    private FishManager fishManager;
    private void Awake()
    {
        this.fishManager = FindObjectOfType<FishManager>();

    }

    private void Start()
    { 
        fishManager.MissedHookFish[this.playerIndex].AddListener(this.MissedHook);
        fishManager.HookedFish[this.playerIndex].AddListener(this.OnHookedFish);
    }

    private void OnHookedFish()
    {
        //pass up the chain
        this.hookedFish?.Invoke();
    }

    private void MissedHook()
    {
        Vector3 fishHookPosition = this.fishHook.position;
        this.fishHook.position.LerpTo(fishHookPosition + new Vector3(0, 0.2f, 0), 0.2f,
            val => this.fishHook.position = val,
            pkg =>
            {
                this.fishHook.position.LerpTo(fishHookPosition, 0.4f, val => this.fishHook.position = val, null, null);
            });
    }

    public void OnArmRaisedValueChanged(float newVal)
    {
        //number below is just arbitrary to make the standard deviation bigger so the values are human readable
        newVal *= 100;
        if (Time.timeSinceLevelLoad - this.armRaiseBufferStartTimestamp >= armBufferTimeLength)
        {
            armRaiseValueBufferPointer = 0;
            this.armRaiseBufferStartTimestamp = Time.timeSinceLevelLoad;
        }

        if (this.armRaiseValueBuffer.Count > armRaiseValueBufferPointer)
        {
            this.armRaiseValueBuffer[this.armRaiseValueBufferPointer] = newVal;
        }
        else
        {
            this.armRaiseValueBuffer.Add(newVal);
        }
        
        this.armRaiseValueBufferPointer++;
    }

    private void Update()
    {
        float squaredStandardDeviation = CalculateStandardDeviationSquared();

        if (squaredStandardDeviation >= this.minFlickDeviation)
        {
            this.fishManager.TryHookClosestFish(this.playerIndex, this.fishHook.position);
        }
    }

    private float CalculateStandardDeviationSquared()
    {
        float armRaiseBufferTotal = this.SumArmRaiseBuffer();
        float meanArmRaise = armRaiseBufferTotal / this.armRaiseValueBuffer.Count;

        float[] varience = this.FindArmRaiseBufferVarience(meanArmRaise);
        float squaredStandardDeviation = varience.Select(x => x * x).Sum() / (this.armRaiseValueBuffer.Count- 1);
        return squaredStandardDeviation;
    }


    private float SumArmRaiseBuffer()
    {
        float sum = 0;
        for (int i = 0; i < this.armRaiseValueBuffer.Count; i++)
        {
            sum += this.armRaiseValueBuffer[i];
        }
        
        return sum;
    }
    private float[] FindArmRaiseBufferVarience(float meanArmRaise)
    {
        float[] varience = new float[this.armRaiseValueBuffer.Count];

        for (int i = 0; i < varience.Length; i++)
        {
            varience[i] = this.armRaiseValueBuffer[i] - meanArmRaise;
        }

        return varience;
    }

    public void ResetValues()
    {
        this.armRaiseValueBuffer.Clear();
        this.armRaiseValueBufferPointer = 0;
        this.armRaiseBufferStartTimestamp = 0;
    }
}
