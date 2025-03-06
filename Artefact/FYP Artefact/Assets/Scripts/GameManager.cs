using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Utility;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector screenCoverDirector;

    [SerializeField] private TimelineAsset coverScreenTimeline;

    [SerializeField] private SceneChanger sceneChanger;
    public async void TimesUp()
    {
        this.screenCoverDirector.playableAsset = this.coverScreenTimeline;
        this.screenCoverDirector.Play();

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        
        GameMode.CangeToEndScreen();
    }
}

