using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlayerSpawning;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[Serializable, CreateAssetMenu]
public class MultiplayerGameplayGameMode : GameplayGameMode
{

    [SerializeField] private GameObject Player1Prefab;

    [SerializeField] private GameObject Player2Prefab;
    public GameObject player1Hat;
    public GameObject player2Hat;
    public eteeDevice player1Device { get; set; }
    public eteeDevice player2Device { get; set; }

    [SerializeField] private RenderTexture player1MainUiRenderTexture, player2MainUiRenderTexture;

    [SerializeField] private PanelSettings player1UiPanelSettings, player2UiPanelSettings;

    [SerializeField] private VisualTreeAsset player2Ui;

    public override void Setup(SceneSpawnPoints spawnPoints)
    {
        base.Setup(spawnPoints);
        this.player1Device = eteeAPI.LeftDevice;
        this.player2Device = eteeAPI.RightDevice;
        
        Vector3 spawnPos = spawnPoints.GetSpawnPoint(PlayerSpawnPoints.Player1);
        GameObject player1 = base.SpawnPlayer(this.Player1Prefab, this.player1Device, spawnPos);
        UIDocument player1MainUiDoc = player1.GetComponentInChildren<UIDocument>();
        player1MainUiDoc.panelSettings = this.player1UiPanelSettings;
        VisualElement clone = base.playerMainUi.CloneTree();
        VisualElement[] children = clone.Children().ToArray();
        for (int i = children.Length - 1; i >= 0; i--)
        {
            player1MainUiDoc.rootVisualElement.Add(children[i]);
        }
        player1.GetComponentInChildren<RawImage>().texture = this.player1MainUiRenderTexture;
        player1.GetComponentInChildren<ClearRenderTexture>().renderTexture = this.player1MainUiRenderTexture;

        spawnPos = spawnPoints.GetSpawnPoint(PlayerSpawnPoints.Player2);
        GameObject player2 = base.SpawnPlayer(this.Player2Prefab, this.player2Device, spawnPos);
        UIDocument player2MainUiDoc = player2.GetComponentInChildren<UIDocument>();
        player2MainUiDoc.panelSettings = this.player2UiPanelSettings;
        clone = this.player2Ui.CloneTree();
        children = clone.Children().ToArray();
        for (int i = children.Length - 1; i >= 0; i--)
        {
            player2MainUiDoc.rootVisualElement.Add(children[i]);
        }
        
        player2.GetComponentInChildren<RawImage>().texture = this.player2MainUiRenderTexture;
        
        base.SpawnRippleSpawner();
    }
}
