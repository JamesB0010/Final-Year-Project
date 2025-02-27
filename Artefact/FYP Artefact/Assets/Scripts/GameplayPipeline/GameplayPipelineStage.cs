using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class GameplayPipelineStage : MonoBehaviour
{
    [SerializeField] private UnityEvent StageEnteredEvent;

    [SerializeField] private DelayedUnityEvent[] delayedStageEnteredEvents;

    [SerializeField] private UnityEvent StageCompleteEvent;


    private PlayerGameplayStagePipeline pipeline;

    private void Awake()
    {
        this.pipeline = GetComponentInParent<PlayerGameplayStagePipeline>();
    }

    public void StageComplete()
    {
        this.StageCompleteEvent?.Invoke();
        this.pipeline.Next();
    }

    public void StageEntered()
    {
        this.StageEnteredEvent?.Invoke();
        foreach (var e in this.delayedStageEnteredEvents)
        {
            e.Invoke();
        }
    }
}
