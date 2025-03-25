using System;
using System.Collections;
using System.Collections.Generic;
using PlayerSpawning;
using UnityEngine;

[Serializable, CreateAssetMenu]
public class MultiplayerGameplayGameMode : GameplayGameMode
{

    [SerializeField] private GameObject Player1Prefab;

    [SerializeField] private GameObject Player2Prefab;
    public GameObject player1Hat;
    public GameObject player2Hat;
    public eteeDevice player1Device { get; set; }
    public eteeDevice player2Device { get; set; }

    public override void Setup(SceneSpawnPoints spawnPoints)
    {
        base.Setup(spawnPoints);
        this.player1Device = eteeAPI.LeftDevice;
        this.player2Device = eteeAPI.RightDevice;
        
        Vector3 spawnPos = spawnPoints.GetSpawnPoint(PlayerSpawnPoints.Player1);
        base.SpawnPlayer(this.Player1Prefab, this.player1Device, spawnPos);

        spawnPos = spawnPoints.GetSpawnPoint(PlayerSpawnPoints.Player2);
        base.SpawnPlayer(this.Player2Prefab, this.player2Device, spawnPos);
        
        base.SpawnRippleSpawner();
    }
}
