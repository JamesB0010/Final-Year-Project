using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
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

[CustomEditor(typeof(GameplayPipelineStage))]
public class PipelineStageEditor : Editor
{
    private GameplayPipelineStage t;

    private void Awake()
    {
        this.t = target as GameplayPipelineStage;
    }

    public override void OnInspectorGUI()
    {
        if (Application.isPlaying)
            this.DrawSkipButton();
        
        base.OnInspectorGUI();
    }

    private void DrawSkipButton()
    {
        if (GUILayout.Button("Skip Stage"))
            t?.StageComplete();
    }
}
