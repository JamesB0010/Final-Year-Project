using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSpawning;

[Serializable, CreateAssetMenu]
public class SinglePlayerGameplayGameMode : GameplayGameMode
{
    public GameObject playerHat;
    [SerializeField] protected GameObject playerPrefab;
    public eteeDevice PlayerDevice { get; private set; }
    public void Initialize(eteeDevice playerDevice)
    {
        this.PlayerDevice = playerDevice;
    }
    
    public override void Setup(SceneSpawnPoints spawnPoints)
    {
        base.Setup(spawnPoints);
        
        if (this.PlayerDevice == null)
            this.PlayerDevice = eteeAPI.LeftDevice;
        
        Vector3 spawnPos = spawnPoints.GetSpawnPoint(PlayerSpawnPoints.Player1);
        GameObject player = base.SpawnPlayer(this.playerPrefab, this.PlayerDevice, spawnPos);
        
        DisableSplitScreenOnPlayer(player);

        this.SpawnRippleSpawner();
    }

    private static void DisableSplitScreenOnPlayer(GameObject player)
    {
        foreach (Camera cam in player.GetComponentsInChildren<Camera>())
        {
            cam.rect = new Rect(0, 0, 1, 1);
        }
    }
}
