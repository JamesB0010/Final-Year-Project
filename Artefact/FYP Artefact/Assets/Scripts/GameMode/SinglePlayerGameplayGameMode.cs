using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PlayerSpawning;
using UnityEngine.Playables;
using UnityEngine.Timeline;
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
    
    [SerializeField] private TimelineAsset uiFishCollectedAnimation;

    public override void Setup(SceneSpawnPoints spawnPoints, PipelineSkipper pipelineSkipper)
    {
        base.Setup(spawnPoints, pipelineSkipper);
        
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

        PlayableDirector uiAnimator = playerMainUiDoc.transform.GetChild(0).GetComponent<PlayableDirector>();
        uiAnimator.playableAsset = this.uiFishCollectedAnimation;
        var signalTrack = ((TimelineAsset)uiAnimator.playableAsset).GetOutputTrack(1) as SignalTrack;
        SignalReceiver uiSignalReciever = playerMainUiDoc.GetComponent<SignalReceiver>();
        
        pipelineSkipper.Setup(player.GetComponentInChildren<PlayerGameplayPipeline>());

        uiAnimator.SetGenericBinding(signalTrack, uiSignalReciever);
        
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
