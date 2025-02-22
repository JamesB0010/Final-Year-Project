using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;


public class RippleSpawner : MonoBehaviour
{
    [SerializeField] private int maxRipples;

    [SerializeField, Tooltip("The spawn rate in seconds of the ripples")] private float spawnRate;

    [SerializeField] private Ripple ripplePrefab;

    private float waterHeight;

    private float lastSpawnedTimeStamp = -90000f;

    private int currentActiveRipples = 0;

    private Bounds spawnBounds;

    public void SetCurrentActiveRippleCount(int count)
    {
        this.currentActiveRipples = count;
    }

    public void Initialize(float waterHeight)
    {
        this.waterHeight = waterHeight;

        this.spawnBounds = new Bounds(transform.position, transform.localScale);
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
        Ripple ripple = Instantiate(this.ripplePrefab, this.GenerateRandomRippleSpawnPosition(), Quaternion.identity);
        ripple.transform.parent = transform;
        ripple.Initialize(this.spawnBounds);
        RippleManager.instance.NewRipple(ripple);
    }

    private Vector3 GenerateRandomRippleSpawnPosition()
    {
        Vector3 spawnPosition = this.spawnBounds.RandomXZInBounds();
        
        return new Vector3(spawnPosition.x, this.waterHeight, spawnPosition.z);
    }
}
