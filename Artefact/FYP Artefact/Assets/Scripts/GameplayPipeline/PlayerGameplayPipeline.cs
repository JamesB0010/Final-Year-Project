using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGameplayPipeline : MonoBehaviour
{
    private GameplayPipelineStage[] gameplayStages;

    private int activeStage = 0;
    private void Awake()
    {
        this.DiscoverGameplayStages();

        this.DisableAllGameplayStages();
        
        //enable first gameplay stage
        this.gameplayStages[0].gameObject.SetActive(true);
    }

    private void DiscoverGameplayStages()
    {
        this.gameplayStages = GetComponentsInChildren<GameplayPipelineStage>();
    }

    private void DisableAllGameplayStages()
    {
        foreach (GameplayPipelineStage stage in gameplayStages)
        {
            stage.gameObject.gameObject.SetActive(false);
        }
    }

    public void SkipCurrentStage()
    {
        this.gameplayStages[this.activeStage].StageComplete();
    }

    /// <summary>
    /// should only be called from a GameplayStage
    /// </summary>
    public void Next()
    {
        this.gameplayStages[this.activeStage].gameObject.SetActive(false);
        this.activeStage = (this.activeStage + 1) % this.gameplayStages.Length;
        this.gameplayStages[this.activeStage].gameObject.SetActive(true);
        
        this.gameplayStages[activeStage].StageEntered();
    }
}

