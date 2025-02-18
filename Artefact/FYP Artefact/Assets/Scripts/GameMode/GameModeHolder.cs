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
                return new SinglePlayerGameMode(eteeAPI.LeftDevice, this.playerPrefab);
            }
        }
        set => this.gameMode = value;
    }

    [SerializeField] public GameObject playerPrefab;
}
