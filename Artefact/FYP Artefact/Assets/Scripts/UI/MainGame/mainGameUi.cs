using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class mainGameUi : MonoBehaviour
{
    private VisualElement uiRoot;

    [SerializeField] private PlayerScores playerScores;
    private TextElement playerScoreText;
    private TextElement countdownText;
    
    [SerializeField] private int playerIndex;

    private void Start()
    {
        this.uiRoot = GetComponent<UIDocument>().rootVisualElement;
        this.playerScoreText = this.uiRoot.Q<TextElement>("FishCount");

        bool isPlayer1 = playerIndex == 0;
        if (isPlayer1)
        {
            this.playerScores.Player1ScoreChanged += this.OnPlayerScoreChange;
        }
        else
        {
            this.playerScores.Player2ScoreChanged += this.OnPlayerScoreChange;
        }

        this.countdownText = this.uiRoot.Q<TextElement>("CountdownValue");
        FindObjectOfType<TimeManager>().IntTick.AddListener(this.OnIntTick);
    }

    private void OnPlayerScoreChange(int newScore)
    {
        this.playerScoreText.text = newScore.ToString();
    }

    private void OnIntTick(int newTime)
    {
        this.countdownText.text = newTime.ToString();
    }
}
