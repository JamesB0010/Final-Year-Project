using UnityEngine;

namespace PlayerSpawning
{
    public class SceneSpawnPoints : MonoBehaviour
    {
        [SerializeField] private Vector3[] spawnPoints;
        public Vector3 GetSpawnPoint(PlayerSpawnPoints spawnPoint)
        {
            return spawnPoints[(byte)spawnPoint];
        }
    }
}