using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable, CreateAssetMenu]
public class SinglePlayerGameMode : GameMode
{
    
    [SerializeField] protected GameObject playerPrefab;
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
        eteeDeviceHolder playerDeviceHolder = player.GetComponent<eteeDeviceHolder>();
        playerDeviceHolder.Device = this.PlayerDevice;

        player.transform.position = spawnPoints.GetSpawnPoint(SceneSpawnPoints.PlayerSpawnPoints.player1);
        
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
