using System;
using PlayerSpawning;
using UnityEngine;
using UnityEngine.Events;

public class MainGameSetup : MonoBehaviour
{
    [SerializeField] private GameModeHolder gameModeHolder;
    [SerializeField] private SceneSpawnPoints mainGameSpawnPoints;
    [SerializeField] private PlayerScores playerScores;
    
    public UnityEvent GameSetupEvent;

    private void Start()
    {
        ((GameplayGameMode)gameModeHolder.GameplayGameMode).Setup(mainGameSpawnPoints);
        this.playerScores.Reset();
        this.GameSetupEvent?.Invoke();
    }
        
}
