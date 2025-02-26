using PlayerSpawning;
using UnityEngine;

public class MainGameSetup : MonoBehaviour
{
    [SerializeField] private GameModeHolder gameModeHolder;
    [SerializeField] private SceneSpawnPoints mainGameSpawnPoints;

    private void Start() => gameModeHolder.GameMode.Setup(mainGameSpawnPoints);
}
