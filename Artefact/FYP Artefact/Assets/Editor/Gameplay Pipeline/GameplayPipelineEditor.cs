using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerGameplayPipeline))]
public class GameplayPipelineEditor : Editor
{
    private PlayerGameplayPipeline t;

    private void Awake()
    {
        this.t = target as PlayerGameplayPipeline;
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
