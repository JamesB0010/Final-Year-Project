using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SinglePlayerHighScoreUpdater : MonoBehaviour
{
    [SerializeField] private PlayerScores playerScores;

    [SerializeField] private UnityEvent newHighscoreDetected;

    private void Start()
    {
        if (PlayerPrefs.GetInt("SinglePlayerHighScore", 0) < playerScores.Player1Score)
        {
            this.newHighscoreDetected?.Invoke();
            PlayerPrefs.SetInt("SinglePlayerHighScore", playerScores.Player1Score);
        }
    }
}
