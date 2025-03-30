using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ReelFishInManager : MonoBehaviour
{
    private FishManager fishManager;

    [SerializeField] private int playerIndex;

    [SerializeField] private ReelInRotator[] reelInRotators;

    private int[] reelLayersCompleteCount = new int[]{ 0, 0, 0 };

    private UniTaskCompletionSource[] layerCompletionTCS = new UniTaskCompletionSource[]
        { new UniTaskCompletionSource(), new UniTaskCompletionSource(), new UniTaskCompletionSource() };

    [SerializeField] private UnityEvent[] layerCompleteEvents;

    [SerializeField] private Transform player;

    [SerializeField] private UnityEvent<Fish> fishReeledInFully;
    
    private void Awake()
    {
        this.fishManager = FindObjectOfType<FishManager>();
    }

    public async void Execute()
    {
        Fish fish = await this.fishManager.MoveTargetFishToStartPoint(this.playerIndex, this.player.position);
        fish.transform.parent = this.reelInRotators[0].transform;
        this.reelInRotators[0].Activate();
        await this.layerCompletionTCS[0].Task;
        Debug.Log("Layer 1 complete!");
        this.layerCompleteEvents[0]?.Invoke();
        fish.transform.parent = this.reelInRotators[1].transform;
        await this.fishManager.MoveFishToStartPoint(fish, this.playerIndex, 1);
        this.reelInRotators[1].Activate();
        await this.layerCompletionTCS[1].Task;
        Debug.Log("Layer 2 complete!");
        this.layerCompleteEvents[1]?.Invoke();
        fish.transform.parent = this.reelInRotators[2].transform;
        await this.fishManager.MoveFishToStartPoint(fish, this.playerIndex, 2);
        this.reelInRotators[2].Activate();
        await this.layerCompletionTCS[2].Task;
        this.layerCompleteEvents[2]?.Invoke();
        Debug.Log("Layer 3 complete!");
        this.fishReeledInFully?.Invoke(fish);
    }

    public void ReelActionComplete(int layer)
    {
        this.reelLayersCompleteCount[layer]++;

        if (this.reelLayersCompleteCount[layer] == 3)
        {
            this.layerCompletionTCS[layer].TrySetResult();
        }
    }

    public void ResetState()
    {
        this.reelLayersCompleteCount = new int[] { 0, 0, 0 };
        this.layerCompletionTCS = new UniTaskCompletionSource[] { new UniTaskCompletionSource(), new UniTaskCompletionSource(), new UniTaskCompletionSource() };
    }
}
