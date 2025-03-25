using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class GameModeHolder : ScriptableObject
{
    [SerializeField, HideInInspector] private GameMode gameplayGameMode;

    public GameMode GameplayGameMode
    {
        get
        {
            if(this.gameplayGameMode != null)
                return this.gameplayGameMode;
            else
            {
                Debug.LogWarning("Using default game mode");
                var defaultMode = Resources.Load<GameplayGameMode>("GameModes/SinglePlayer");
                SinglePlayerGameplayGameMode castedMode = defaultMode as SinglePlayerGameplayGameMode;
                castedMode.Initialize(eteeAPI.LeftDevice);
                this.gameplayGameMode = defaultMode;
                return this.gameplayGameMode;
            }
        }
        set => this.gameplayGameMode = value;
    }
}

