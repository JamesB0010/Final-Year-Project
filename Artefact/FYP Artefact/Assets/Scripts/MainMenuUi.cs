using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField] private eteeTrackpadEvents eteeTrackpadEvents;
    
    private VisualElement UiRoot;
    private VisualElement[] rotatingStars = new VisualElement[2];
    private SqueezeSelectButton playSinglePlayerContainer;
    private SqueezeSelectButton playMultiplayerContainer;
    private SqueezeSelectButton [] playOptionContainers = new SqueezeSelectButton[2];
    private int leftSelectedContainer = 0;
    [SerializeField] private FingerTotalForceGetter fingerTotalForceGetter;
    private VisualElement screenCover;
    [SerializeField] private TimelineAsset fillScreenTimeline;

    private int LeftSelectedContainer
    {
        get => this.leftSelectedContainer;
        set
        {
            bool noChangeDetected = this.leftSelectedContainer == value;
            if (noChangeDetected)
                return;

            this.playOptionContainers[this.leftSelectedContainer].RemoveFromClassList("Player1Focused");
            this.playOptionContainers[this.leftSelectedContainer].Player1SqueezeAmount = 0;

            this.leftSelectedContainer = value;

            if (value == -1)
            {
                this.leftSelectedContainer = 1;
            }

            this.leftSelectedContainer %= 2;

            this.playOptionContainers[this.leftSelectedContainer].AddToClassList("Player1Focused");
        }
    }
    private int rightSelectedContainer = 0;

    private int RightSelectedContainer
    {
        get => this.rightSelectedContainer;
        set
        {
            bool noChangeDetected = this.rightSelectedContainer== value;
            if (noChangeDetected)
                return;
 
            this.playOptionContainers[this.rightSelectedContainer].RemoveFromClassList("Player2Focused");
            this.playOptionContainers[this.rightSelectedContainer].Player2SqueezeAmount = 0;

            this.rightSelectedContainer= value;

            if (value == -1)
            {
                this.rightSelectedContainer= 1;
            }

            this.rightSelectedContainer %= 2;
            
            this.playOptionContainers[this.rightSelectedContainer].AddToClassList("Player2Focused");
        }
    }
    
    private void Awake()
    {
        this.UiRoot = GetComponent<UIDocument>().rootVisualElement;
        this.SetupDependencies();
        this.playOptionContainers[0] = this.playSinglePlayerContainer;
        this.playOptionContainers[1] = this.playMultiplayerContainer;
    }

    private void Update()
    {
        this.playOptionContainers[this.leftSelectedContainer].Player1SqueezeAmount = (uint)this.fingerTotalForceGetter.GetGenerousPullPercent(0);
        this.playOptionContainers[this.rightSelectedContainer].Player2SqueezeAmount = (uint)this.fingerTotalForceGetter.GetGenerousPullPercent(1);
    }

    private void SetupDependencies()
    {
        this.screenCover = this.UiRoot.Q<VisualElement>("ScreenCover");
        this.rotatingStars[0] = this.UiRoot.Q<VisualElement>("RotatingStar1");
        this.rotatingStars[1] = this.UiRoot.Q<VisualElement>("RotatingStar2");
        this.playSinglePlayerContainer = this.UiRoot.Q<SqueezeSelectButton>("PlaySinglePlayerContainer");
        this.playMultiplayerContainer = this.UiRoot.Q<SqueezeSelectButton>("PlayMultiplayerContainer");

        this.playSinglePlayerContainer.ButtonPressed += PlaySinglePlayerPressed;
        this.playMultiplayerContainer.ButtonPressed += PlayMultiplayerPressed;
    }

    private void PlaySinglePlayerPressed()
    {
        Debug.Log("Play singleplayer");
        CoverScreen();
        StartCoroutine(this.ChangeSceneAfter(0.5f, "Main Scene"));
    }

    private void CoverScreen()
    {
        PlayableDirector director = GetComponentInChildren<PlayableDirector>();
        director.playableAsset = this.fillScreenTimeline;
        director.Play();
    }

    private void PlayMultiplayerPressed()
    {
        Debug.Log("Play multiplayer");
        CoverScreen();
        StartCoroutine(this.ChangeSceneAfter(0.5f, "MindOurDust"));
    }

    public IEnumerator ChangeSceneAfter(float timeToWait, string scene)
    {
        yield return new WaitForSeconds(timeToWait);

        SceneManager.LoadScene(scene);
    }

    private void Start()
    {
        this.RotateStars();
        this.PingPongScaleTitle();
        this.playSinglePlayerContainer.AddToClassList("Player1Focused");
        this.playSinglePlayerContainer.AddToClassList("Player2Focused");
        Invoke(nameof(this.SlideInInteractionButtons), 1f);

        Invoke(nameof(this.ListenToTrackpadEvents), 0.1f);
    }

    private void ListenToTrackpadEvents()
    {
        this.eteeTrackpadEvents.LeftTrackpadEvents.up += LeftDeviceTrackpadUp;
        this.eteeTrackpadEvents.LeftTrackpadEvents.down += LeftDeviceTrackpadDown;

        this.eteeTrackpadEvents.RightTrackpadEvents.up += RightDeviceTrackpadUp;
        this.eteeTrackpadEvents.RightTrackpadEvents.down += RightDeviceTrackpadDown;
    }

    private void RightDeviceTrackpadDown()
    {
        this.RightSelectedContainer++;
    }

    private void RightDeviceTrackpadUp()
    {
        this.RightSelectedContainer--;
    }


    private void LeftDeviceTrackpadUp()
    {
        this.LeftSelectedContainer--;
    }

    private void LeftDeviceTrackpadDown()
    {
        this.LeftSelectedContainer++;
    }
    private void SlideInInteractionButtons()
    {
        this.playSinglePlayerContainer.RemoveFromClassList("MoveInteractionOffScreen");
        this.playMultiplayerContainer.RemoveFromClassList("MoveInteractionOffScreen");
    }


    private void RotateStars()
    {
        float rotationSpeed = 1.5f;
        for (int i = 0; i < this.rotatingStars.Length; i++)
        {
            VisualElement star = this.rotatingStars[i];
            FloatLerpPackage rotationLerpPackage = new FloatLerpPackage(-35f, 35f, value =>
            {
                star.transform.rotation = Quaternion.Euler(value * Vector3.forward);
            }, pkg =>
            {
                pkg.Reverse();
                GlobalLerpProcessor.AddLerpPackage(pkg);
            }, rotationSpeed, GlobalLerpProcessor.easeInOutCurve);

            FloatLerpPackage scaleLerpPackage = new FloatLerpPackage(1f, 1.3f, value =>
            {
                star.transform.scale = new Vector3(value, value, value);
            }, pkg =>
            {
                pkg.Reverse();
                GlobalLerpProcessor.AddLerpPackage(pkg);
            }, rotationSpeed / 2, GlobalLerpProcessor.easeInOutCurve);
            
            
            if(i == 1)
                rotationLerpPackage.Reverse();
            
            GlobalLerpProcessor.AddLerpPackage(rotationLerpPackage);
            GlobalLerpProcessor.AddLerpPackage(scaleLerpPackage);
        }
    }
    
    private void PingPongScaleTitle()
    {
        Label titleLabel = this.UiRoot.Q<Label>("title");
        titleLabel.transform.scale.LerpTo(new Vector3(1.2f, 1.2f), 3, value =>
        {
            titleLabel.transform.scale = value;
        }, pkg =>
        {
            pkg.Reverse();
            GlobalLerpProcessor.AddLerpPackage(pkg);
        }, GlobalLerpProcessor.easeInOutCurve);
    }
}
