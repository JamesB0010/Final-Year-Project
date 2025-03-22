using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerGameplayStagePipeline))]
public class GameplayPipelineEditor : Editor
{
    private PlayerGameplayStagePipeline t;

    private void Awake()
    {
        this.t = target as PlayerGameplayStagePipeline;
    }

    public override void OnInspectorGUI()
    {
        DrawSkipStageButton();
        base.OnInspectorGUI();
    }

    private void DrawSkipStageButton()
    {
        if (Application.isPlaying)
        {
            if (GUILayout.Button("Skip gameplay stage"))
            {
                t.SkipCurrentStage();
            }
        }
    }
}
