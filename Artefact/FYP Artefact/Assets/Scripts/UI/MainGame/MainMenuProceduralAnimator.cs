using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuProceduralAnimator : MonoBehaviour
{
    [SerializeField] private float starRotationSpeed;
    private VisualElement root;
    private readonly List<VisualElement> stars = new List<VisualElement>();

    private void Awake()
    {
        //setup dependencies
        root = GetComponent<UIDocument>().rootVisualElement;
        stars.Add(root.Q<VisualElement>("RotatingStar1"));
        stars.Add(root.Q<VisualElement>("RotatingStar2"));
    }

    private void Start()
    {
        this.RotateStars();
        this.PingPongScaleTitle();
        Invoke(nameof(this.slideInInteractionButtons), 1f);
    }

    private void RotateStars()
    {
        for(var i = 0; i < this.stars.Count; i++)
        {
            VisualElement star = this.stars[i];
            this.StartStareScaleAnimation(star);
            var starRotationAnimation = this.StartStarRotationAnimation(star);
            
            if(i == 1)
                starRotationAnimation.Reverse();
        }

    }


    private void StartStareScaleAnimation(VisualElement star)
    {
        FloatLerpPackage scaleLerpPackage = new FloatLerpPackage(1f, 1.3f, val =>
        {
            star.transform.scale = new Vector3(val, val);
        }, pkg =>
        {
            pkg.Reverse();
            GlobalLerpProcessor.AddLerpPackage(pkg);
        }, this.starRotationSpeed / 2, GlobalLerpProcessor.easeInOutCurve);
        
        GlobalLerpProcessor.AddLerpPackage(scaleLerpPackage);
    }

    private FloatLerpPackage StartStarRotationAnimation(VisualElement star)
    {
        FloatLerpPackage rotationLerpPackage = new FloatLerpPackage(-35f, 35f, value =>
            {
                star.transform.rotation = Quaternion.Euler(value * Vector3.forward);
            }, pkg =>
            {
                pkg.Reverse();
                GlobalLerpProcessor.AddLerpPackage(pkg);
            }, this.starRotationSpeed, GlobalLerpProcessor.easeInOutCurve);

        GlobalLerpProcessor.AddLerpPackage(rotationLerpPackage);
        return rotationLerpPackage;
    }
    private void PingPongScaleTitle()
    {
        Label titleLabel = this.root.Q<Label>("title");
        titleLabel.transform.scale.LerpTo(new Vector3(1.2f,1.2f), 3, val =>
        {
            titleLabel.transform.scale = val;
        }, pkg =>
        {
            pkg.Reverse();
            GlobalLerpProcessor.AddLerpPackage(pkg);
        }, GlobalLerpProcessor.easeInOutCurve);
    }
    private void slideInInteractionButtons()
    {
        this.root.Q<SqueezeSelectButton>("PlaySinglePlayerContainer").RemoveFromClassList("MoveInteractionOffScreen");
        this.root.Q<SqueezeSelectButton>("PlayMultiplayerContainer").RemoveFromClassList("MoveInteractionOffScreen");
    }
}
