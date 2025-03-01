using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class mainGameUi : MonoBehaviour
{
    private VisualElement uiRoot;

    [SerializeField] private PlayerScores playerScores;
    private TextElement player1ScoreText;
    private TextElement countdownText;

    private void Awake()
    {
        this.uiRoot = GetComponent<UIDocument>().rootVisualElement;
        this.player1ScoreText = this.uiRoot.Q<TextElement>("FishCount");
        this.playerScores.Player1ScoreChanged += this.OnPlayer1ScoreChange;

        this.countdownText = this.uiRoot.Q<TextElement>("CountdownValue");
        FindObjectOfType<TimeManager>().IntTick.AddListener(this.OnIntTick);
    }

    private void OnPlayer1ScoreChange(int newScore)
    {
        this.player1ScoreText.text = newScore.ToString();
    }

    private void OnIntTick(int newTime)
    {
        this.countdownText.text = newTime.ToString();
    }
}
