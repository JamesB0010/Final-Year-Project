using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameMode : ScriptableObject
{
    [SerializeField] protected RippleSpawner rippleSpawnerPrefab;

    [SerializeField] private Vector3 rippleSpawnerSpawnLocation;

    [SerializeField] private float rippleSpawnerYRotation;

    [SerializeField] private float waterHeight;
    
    [SerializeField] protected GameObject playerPrefab;

    public virtual void Setup(SceneSpawnPoints spawnPoints)
    {
        
    }

    protected void SpawnRippleSpawner()
    {
        RippleSpawner rippleSpawner = Instantiate(this.rippleSpawnerPrefab, this.rippleSpawnerSpawnLocation, Quaternion.Euler(0, this.rippleSpawnerYRotation, 0));
        rippleSpawner.Initialize(this.waterHeight);
        
    }
}
