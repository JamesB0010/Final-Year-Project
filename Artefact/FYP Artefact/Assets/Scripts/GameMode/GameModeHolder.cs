using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameModeHolder : ScriptableObject
{
    private GameMode gameMode;

    public GameMode GameMode
    {
        get
        {
            if(this.gameMode != null)
                return this.gameMode;
            else
            {
                Debug.LogWarning("Using default game mode");
                var GameMode = Resources.Load<GameMode>("GameModes/SinglePlayer");
                ((SinglePlayerGameMode)GameMode).Initialize(eteeAPI.LeftDevice);
                return GameMode;
            }
        }
        set => this.gameMode = value;
    }

    [SerializeField] public GameObject playerPrefab;
}

