using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RippleSpawner : MonoBehaviour
{
    [SerializeField] private int maxRipples;

    [SerializeField, Tooltip("The spawn rate in seconds of the ripples")] private float spawnRate;

    [SerializeField] private Ripple ripplePrefab;

    private float waterHeight;

    private float lastSpawnedTimeStamp = -90000f;

    private int currentActiveRipples = 0;

    public void Initialize(float waterHeight)
    {
        this.waterHeight = waterHeight;
    }

    private void Update()
    {
        TrySpawnRipples();
    }

    private void TrySpawnRipples()
    {
        if (currentActiveRipples == maxRipples)
            return;

        if (Time.timeSinceLevelLoad - lastSpawnedTimeStamp >= spawnRate)
            this.SpawnRipple();
    }

    private void SpawnRipple()
    {
        this.lastSpawnedTimeStamp = Time.timeSinceLevelLoad;
        this.currentActiveRipples++;
        Debug.Log("Spawn ripple");
    }
}
