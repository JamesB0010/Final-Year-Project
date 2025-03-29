using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ReelFishInManager : MonoBehaviour
{
    private FishManager fishManager;

    [SerializeField] private int playerIndex;

    [SerializeField] private ReelInRotator[] reelInRotators;
    
    private void Awake()
    {
        this.fishManager = FindObjectOfType<FishManager>();
    }

    public async void Execute()
    {
        Fish fish = await this.fishManager.MoveTargetFishToStartPoint(this.playerIndex);
        fish.transform.parent = this.reelInRotators[0].transform;
        this.reelInRotators[0].Activate();
    }
}
