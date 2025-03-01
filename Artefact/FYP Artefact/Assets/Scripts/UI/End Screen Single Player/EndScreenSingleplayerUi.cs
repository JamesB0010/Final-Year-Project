using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class EndScreenSingleplayerUi : MonoBehaviour
{
    private VisualElement root;

    [SerializeField] private PlayerScores playerScores;
    private SqueezeSelectButton ReturnToMenuButton { get; set; }

    [SerializeField] private UnityEvent ReturnToMenuPressed;
    
    private void Awake()
    {
        this.root = GetComponent<UIDocument>().rootVisualElement;

        this.root.Q<TextElement>("CaughtAmount").text = $"You caught {playerScores.Player1Score} fish!";

        this.ReturnToMenuButton = this.root.Q<SqueezeSelectButton>("ReturnToMainMenu");

        this.ReturnToMenuButton.Player1Pressed += () =>
        {
            this.ReturnToMenuPressed?.Invoke();
        };
    }


    private void Update()
    {
        this.ReturnToMenuButton.Player1SqueezeAmount = (uint)FingerTotalForceGetter.GetGenerousPullPercent(eteeAPI.LeftDevice);
    }
}
