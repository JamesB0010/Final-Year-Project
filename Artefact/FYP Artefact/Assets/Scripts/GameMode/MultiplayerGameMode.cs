using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu]
public class MultiplayerGameMode : GameMode
{

    [SerializeField] private GameObject Player1Prefab;

    [SerializeField] private GameObject Player2Prefab;
    public eteeDevice player1Device { get; set; }
    public eteeDevice player2Device { get; set; }

    public override void Setup(SceneSpawnPoints spawnPoints)
    {
        this.player1Device = eteeAPI.LeftDevice;
        this.player2Device = eteeAPI.RightDevice;
                
        GameObject player1 = GameObject.Instantiate(this.Player1Prefab);
        eteeDeviceHolder playerDeviceHolder = player1.GetComponent<eteeDeviceHolder>();
        playerDeviceHolder.Device = this.player1Device;
        player1.transform.position = spawnPoints.GetSpawnPoint(SceneSpawnPoints.PlayerSpawnPoints.player1);
        
        
        GameObject player2 = GameObject.Instantiate(this.Player2Prefab);
        playerDeviceHolder = player2.GetComponent<eteeDeviceHolder>();
        playerDeviceHolder.Device = this.player2Device;
        player2.transform.position = spawnPoints.GetSpawnPoint(SceneSpawnPoints.PlayerSpawnPoints.player2);
        
        this.SpawnRippleSpawner();
    }
}
