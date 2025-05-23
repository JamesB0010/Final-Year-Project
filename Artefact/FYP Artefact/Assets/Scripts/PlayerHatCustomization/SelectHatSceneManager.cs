using System;
using System.Collections;
using System.Collections.Generic;
using eteeDeviceInput;
using UnityEngine;
using UnityEngine.Events;

public class SelectHatSceneManager : MonoBehaviour
{
    [SerializeField] private HatSelectGameplayGameMode gameMode;

    [SerializeField] private HatChooser hatChooser;

    [SerializeField] private UiPlayerInput uiPlayerInput;

    [SerializeField] private PlayerHatDataHolder playerHatData;

    [SerializeField] private UnityEvent playerSwipeTrackpadLeft;
    [SerializeField] private UnityEvent playerSwipeTrackpadRight;

    private void Awake()
    {
        if (gameMode.PlayerDevice == null)
            this.gameMode.Initialize(eteeAPI.LeftDevice);
    }

    private void Start()
    {
        if (gameMode.PlayerDevice == eteeAPI.LeftDevice)
        {
            uiPlayerInput.leftEvents.TrackpadSwipeLeft.AddListener(hatChooser.PreviousHat);
            uiPlayerInput.leftEvents.TrackpadSwipeLeft.AddListener(() => this.playerSwipeTrackpadLeft?.Invoke());
            
            uiPlayerInput.leftEvents.TrackpadSwipeRight.AddListener(hatChooser.NextHat);
            uiPlayerInput.leftEvents.TrackpadSwipeRight.AddListener(() => this.playerSwipeTrackpadRight?.Invoke());
            
            hatChooser.newHatSelected?.AddListener(this.playerHatData.SetLeftControllerHatIndex);
            hatChooser.noHatSelected?.AddListener(() => this.playerHatData.SetLeftControllerHatIndex(-1));

            hatChooser.HatIndex = this.playerHatData.leftControllerHatIndex;
        }
        else
        {
            uiPlayerInput.rightEvents.TrackpadSwipeLeft.AddListener(hatChooser.PreviousHat);
            uiPlayerInput.leftEvents.TrackpadSwipeLeft.AddListener(() => this.playerSwipeTrackpadLeft?.Invoke());
            
            uiPlayerInput.rightEvents.TrackpadSwipeRight.AddListener(hatChooser.NextHat);
            uiPlayerInput.leftEvents.TrackpadSwipeRight.AddListener(() => this.playerSwipeTrackpadRight?.Invoke());
            
            hatChooser.newHatSelected?.AddListener(this.playerHatData.SetRightControllerHatIndex);
            hatChooser.noHatSelected?.AddListener(() => this.playerHatData.SetRightControllerHatIndex(-1));
                        
            hatChooser.HatIndex = this.playerHatData.rightControllerHatIndex;
        }
    }
}
