using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpawning
{
    public class SpawnPointsVisualizer : MonoBehaviour
    {
        [SerializeField] private Mesh playerMesh;

        [SerializeField] private Color spawnPointDrawColor;

        [SerializeField] private Vector3 spawnPointVisualisationRotation;

        private SceneSpawnPoints sceneSpawnPoints;
        
        private IEnumerable<PlayerSpawnPoints> playerSpawnPoints = EnumUtility.GetValues<PlayerSpawnPoints>();

        private Vector3 visualizationMeshScale = new Vector3(0.00852519f, 0.00852519f, 0.00852519f);

        private void OnValidate() => this.sceneSpawnPoints = GetComponent<SceneSpawnPoints>();

        private void OnDrawGizmosSelected()
        {
            foreach (PlayerSpawnPoints spawnPoint in this.playerSpawnPoints)
            {
                Gizmos.color = this.spawnPointDrawColor;
                
                Vector3 spawnPos = this.sceneSpawnPoints.GetSpawnPoint(spawnPoint);
                Quaternion spawnRot = Quaternion.Euler(this.spawnPointVisualisationRotation);
                Gizmos.DrawWireMesh(this.playerMesh, spawnPos,spawnRot, this.visualizationMeshScale);
            }
        }
    }
}