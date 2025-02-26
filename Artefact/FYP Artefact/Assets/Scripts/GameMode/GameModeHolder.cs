using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameModeHolder : ScriptableObject
{
    [SerializeField, HideInInspector] private GameMode gameMode;

    public GameMode GameMode
    {
        get
        {
            if(this.gameMode != null)
                return this.gameMode;
            else
            {
                Debug.LogWarning("Using default game mode");
                var defaultMode = Resources.Load<GameMode>("GameModes/SinglePlayer");
                SinglePlayerGameMode castedMode = defaultMode as SinglePlayerGameMode;
                castedMode.Initialize(eteeAPI.LeftDevice);
                this.gameMode = defaultMode;
                return this.gameMode;
            }
        }
        set => this.gameMode = value;
    }
}

