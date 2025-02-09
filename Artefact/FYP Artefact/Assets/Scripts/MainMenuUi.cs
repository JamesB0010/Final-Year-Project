using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuUi : MonoBehaviour
{
    private VisualElement UiRoot;
    private VisualElement[] rotatingStars = new VisualElement[2];
    private void Awake()
    {
        this.UiRoot = GetComponent<UIDocument>().rootVisualElement;
        this.SetupDependencies();
    }

    private void SetupDependencies()
    {
        this.rotatingStars[0] = this.UiRoot.Q<VisualElement>("RotatingStar1");
        this.rotatingStars[1] = this.UiRoot.Q<VisualElement>("RotatingStar2");
    }

    private void Start()
    {
        this.RotateStars();
        this.PingPongScaleTitle();
        Invoke(nameof(this.SlideInInteractionButtons), 1f);
    }

    private void SlideInInteractionButtons()
    {
        this.UiRoot.Q<VisualElement>("PlaySinglePlayerContainer").RemoveFromClassList("MoveInteractionOffScreen");
        this.UiRoot.Q<VisualElement>("PlayMultiplayerContainer").RemoveFromClassList("MoveInteractionOffScreen");
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
