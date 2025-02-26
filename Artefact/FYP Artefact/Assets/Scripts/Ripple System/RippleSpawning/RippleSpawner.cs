using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


public class RippleSpawner : MonoBehaviour
{

    [SerializeField, Tooltip("The spawn rate in seconds of the ripples")] private float spawnRate;

    [SerializeField] private Ripple ripplePrefab;
    
    private float lastSpawnedTimeStamp = -90000f;

    private Bounds spawnBounds;

    public void Initialize()
    {
        this.spawnBounds = new Bounds(transform.position, transform.localScale);
    }

    private void Update()
    {
        TrySpawnRipples();
    }

    private void TrySpawnRipples()
    {
        if (RippleManager.RipplesFull)
            return;

        bool spawnDeltaElapsed = Time.timeSinceLevelLoad - lastSpawnedTimeStamp >= spawnRate;
        if (spawnDeltaElapsed)
            this.SpawnRipple();
    }

    private void SpawnRipple()
    {
        this.lastSpawnedTimeStamp = Time.timeSinceLevelLoad;
        Ripple ripple = Instantiate(this.ripplePrefab, this.GenerateRandomRippleSpawnPosition(), Quaternion.identity);
        ripple.transform.parent = transform;
        ripple.Initialize(this.spawnBounds);
    }

    private Vector3 GenerateRandomRippleSpawnPosition()
    {
        Vector3 spawnPosition = this.spawnBounds.RandomXZInBounds();
        
        return new Vector3(spawnPosition.x, GameMode.CurrentGameMode.WaterHeight, spawnPosition.z);
    }
}
