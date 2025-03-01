using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Utility;

public class ReturnToMainMenuProcess : MonoBehaviour
{
    [SerializeField] private SceneChanger sceneChanger;
    
    [SerializeField] private PlayableDirector coverScreenDirector;

    [SerializeField] private TimelineAsset coverScreenTimeline;

    public async void Execute()
    {
        this.coverScreenDirector.playableAsset = this.coverScreenTimeline;
        
        this.coverScreenDirector.Play();

        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
     
        sceneChanger.ToMainMenu();
    }
}
