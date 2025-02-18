using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerGameMode : GameMode
{
    public eteeDevice player1Device { get; set; }
    public eteeDevice player2Device { get; set; }

    private readonly GameObject playerPrefab;
    public MultiplayerGameMode(GameObject playerPrefab)
    {
        this.player1Device = eteeAPI.LeftDevice;
        this.player2Device = eteeAPI.RightDevice;

        this.playerPrefab = playerPrefab;
    }
    public override void Setup(SceneSpawnPoints spawnPoints)
    {
        GameObject player1 = GameObject.Instantiate(this.playerPrefab);
        RotatePlayer rotatePlayer = player1.GetComponent<RotatePlayer>();
        rotatePlayer.Device = this.player1Device;
        player1.transform.position = spawnPoints.GetSpawnPoint(SceneSpawnPoints.PlayerSpawnPoints.player1);
        
        
        GameObject player2 = GameObject.Instantiate(this.playerPrefab);
        rotatePlayer = player2.GetComponent<RotatePlayer>();
        rotatePlayer.Device = this.player2Device;
        player2.transform.position = spawnPoints.GetSpawnPoint(SceneSpawnPoints.PlayerSpawnPoints.player2);
    }
}
