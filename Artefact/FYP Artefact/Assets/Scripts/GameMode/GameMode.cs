using System;
using PlayerSpawning;
using UnityEngine;

[Serializable]
public abstract class GameMode : ScriptableObject
{
    [SerializeField] protected RippleSpawner rippleSpawnerPrefab;

    [SerializeField] private Vector3 rippleSpawnerSpawnLocation;

    [SerializeField] private float rippleSpawnerYRotation;

    [SerializeField] private float waterHeight;

    public float WaterHeight => this.waterHeight;

    [SerializeField] private int maxRippleCount;
    public int MaxRippleCount => this.maxRippleCount;

    //A game mode is classed as being current from when setup is called
    public static GameMode CurrentGameMode { get; private set; }
    public virtual void Setup(SceneSpawnPoints spawnPoints)
    {
        CurrentGameMode = this;
    }

    protected void SpawnRippleSpawner()
    {
        RippleManager.MaxRipples = this.maxRippleCount;
        RippleSpawner rippleSpawner = Instantiate(this.rippleSpawnerPrefab, this.rippleSpawnerSpawnLocation, Quaternion.Euler(0, this.rippleSpawnerYRotation, 0));
        rippleSpawner.Initialize();
    }

    protected GameObject SpawnPlayer(GameObject playerPrefab, eteeDevice device, Vector3 spawnPoint)
    {
        GameObject player = GameObject.Instantiate(playerPrefab);
        eteeDeviceHolder playerDeviceHolder = player.GetComponent<eteeDeviceHolder>();
        playerDeviceHolder.Device = device;
        player.transform.position = spawnPoint;
        return player;
    }
}
