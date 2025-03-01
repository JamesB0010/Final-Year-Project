using PlayerSpawning;
using UnityEngine;

public class MainGameSetup : MonoBehaviour
{
    [SerializeField] private GameModeHolder gameModeHolder;
    [SerializeField] private SceneSpawnPoints mainGameSpawnPoints;
    [SerializeField] private PlayerScores playerScores;

    private void Start()
    {
        gameModeHolder.GameMode.Setup(mainGameSpawnPoints);
        this.playerScores.Reset();
    }
        
}
