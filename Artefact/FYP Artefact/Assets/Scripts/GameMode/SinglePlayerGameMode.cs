using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerGameMode : GameMode
{
    public SinglePlayerGameMode(eteeDevice playerDevice, GameObject playerPrefab)
    {
        this.PlayerDevice = playerDevice;
        this.playerPrefab = playerPrefab;
    }
    public eteeDevice PlayerDevice { get; private set; }

    public GameObject playerPrefab;
    
    
    public override void Setup(SceneSpawnPoints spawnPoints)
    {
        GameObject player = GameObject.Instantiate(this.playerPrefab);
        RotatePlayer rotatePlayer = player.GetComponent<RotatePlayer>();
        rotatePlayer.Device = this.PlayerDevice;

        player.transform.position = spawnPoints.GetSpawnPoint(SceneSpawnPoints.PlayerSpawnPoints.player2);
    }
}
