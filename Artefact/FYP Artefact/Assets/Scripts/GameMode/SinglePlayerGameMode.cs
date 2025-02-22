using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable, CreateAssetMenu]
public class SinglePlayerGameMode : GameMode
{
    public eteeDevice PlayerDevice { get; private set; }
    public void Initialize(eteeDevice playerDevice)
    {
        this.PlayerDevice = playerDevice;
    }
    
    
    public override void Setup(SceneSpawnPoints spawnPoints)
    {
        if (this.PlayerDevice == null)
            this.PlayerDevice = eteeAPI.LeftDevice;
        
        GameObject player = GameObject.Instantiate(this.playerPrefab);
        RotatePlayer rotatePlayer = player.GetComponent<RotatePlayer>();
        rotatePlayer.Device = this.PlayerDevice;

        player.transform.position = spawnPoints.GetSpawnPoint(SceneSpawnPoints.PlayerSpawnPoints.player1);
        
        this.SpawnRippleSpawner();
    }

    
}
