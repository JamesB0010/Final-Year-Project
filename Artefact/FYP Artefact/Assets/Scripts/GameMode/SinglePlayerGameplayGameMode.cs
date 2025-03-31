using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PlayerSpawning;
using UnityEngine.UI;
using UnityEngine.UIElements;

[Serializable, CreateAssetMenu]
public class SinglePlayerGameplayGameMode : GameplayGameMode
{
    public GameObject playerHat;
    [SerializeField] protected GameObject playerPrefab;
    public eteeDevice PlayerDevice { get; private set; }
    public void Initialize(eteeDevice playerDevice)
    {
        this.PlayerDevice = playerDevice;
    }
    
    [SerializeField] private RenderTexture playerUiRenderTexture;

    [SerializeField] private PanelSettings playerUiPanelSettings;
    
    public override void Setup(SceneSpawnPoints spawnPoints)
    {
        base.Setup(spawnPoints);
        
        if (this.PlayerDevice == null)
            this.PlayerDevice = eteeAPI.LeftDevice;
        
        Vector3 spawnPos = spawnPoints.GetSpawnPoint(PlayerSpawnPoints.Player1);
        GameObject player = base.SpawnPlayer(this.playerPrefab, this.PlayerDevice, spawnPos);
        UIDocument playerMainUiDoc = player.GetComponentInChildren<UIDocument>();
        playerMainUiDoc.panelSettings = this.playerUiPanelSettings;
        VisualElement clone = base.playerMainUi.CloneTree();
        VisualElement[] children = clone.Children().ToArray();
        for (int i = children.Length - 1; i >= 0; i--)
        {
            playerMainUiDoc.rootVisualElement.Add(children[i]);
        }

        player.GetComponentInChildren<RawImage>().texture = this.playerUiRenderTexture;
        
        DisableSplitScreenOnPlayer(player);

        this.SpawnRippleSpawner();
    }

    private static void DisableSplitScreenOnPlayer(GameObject player)
    {
        foreach (Camera cam in player.GetComponentsInChildren<Camera>())
        {
            cam.rect = new Rect(0, 0, 1, 1);
        }
    }
}
