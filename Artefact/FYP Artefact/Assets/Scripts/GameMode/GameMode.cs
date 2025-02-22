using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameMode : ScriptableObject
{
    [SerializeField] protected RippleSpawner rippleSpawnerPrefab;

    [SerializeField] private Vector3 rippleSpawnerSpawnLocation;
    
    [SerializeField] protected GameObject playerPrefab;

    public virtual void Setup(SceneSpawnPoints spawnPoints)
    {
        
    }

    protected void SpawnRippleSpawner()
    {
        //Instantiate(this.rippleSpawnerPrefab, this.rippleSpawnerSpawnLocation, Quaternion.identity);
    }
}
