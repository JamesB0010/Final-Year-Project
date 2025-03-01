using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class PlayerScores : ScriptableObject
{
    [SerializeField] private int player1Score, player2Score;

    public event Action<int> Player1ScoreChanged;
    
    public void IncrementPlayer1Score()
    {
        this.player1Score++;
        this.Player1ScoreChanged?.Invoke(this.player1Score);
    }

    public void Reset()
    {
        this.player1Score = 0;
        this.player2Score = 0;
    }
}
