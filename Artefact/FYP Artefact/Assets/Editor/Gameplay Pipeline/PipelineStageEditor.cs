using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

