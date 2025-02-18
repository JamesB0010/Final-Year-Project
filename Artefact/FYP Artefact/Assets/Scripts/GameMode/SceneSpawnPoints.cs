using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawnPoints : MonoBehaviour
{
    public enum PlayerSpawnPoints : byte
    {
        player1,
        player2
    }
    [SerializeField] private Vector3[] spawnPoints;

    [Space(5)]
    [Header("Visualisation")]
    [SerializeField] private Mesh playerMesh;

    [SerializeField] private Color spawnPointDrawColor;
    
    [SerializeField] private Vector3 spawnPointVisualisationRotation;

    public Vector3 GetSpawnPoint(PlayerSpawnPoints spawnPoint)
    {
        return spawnPoints[(byte)spawnPoint];
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Vector3 sp in spawnPoints)
        {
            Gizmos.color = this.spawnPointDrawColor;
            Gizmos.DrawWireMesh(this.playerMesh, sp, Quaternion.Euler(this.spawnPointVisualisationRotation), new Vector3(0.00852519f,0.00852519f,0.00852519f));
        }
    }
}
