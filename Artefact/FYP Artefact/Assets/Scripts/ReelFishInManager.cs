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
    
    private void Awake()
    {
        this.fishManager = FindObjectOfType<FishManager>();
    }

    public async void Execute()
    {
        Fish fish = await this.fishManager.MoveTargetFishToStartPoint(this.playerIndex);
        fish.transform.parent = this.reelInRotators[0].transform;
        this.reelInRotators[0].Activate();
        await this.layerCompletionTCS[0].Task;
        this.layerCompleteEvents[0]?.Invoke();
        fish.transform.parent = this.reelInRotators[1].transform;
        Debug.Log("Layer 0 complete!");
        await this.fishManager.MoveFishToStartPoint(fish, this.playerIndex, 1);
        //this.reelInRotators[1].Activate();
    }

    public void ReelActionComplete(int layer)
    {
        this.reelLayersCompleteCount[layer]++;

        if (this.reelLayersCompleteCount[layer] == 3)
        {
            this.layerCompletionTCS[layer].TrySetResult();
        }
    }
}
