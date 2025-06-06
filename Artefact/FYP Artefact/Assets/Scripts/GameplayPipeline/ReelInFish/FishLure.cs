using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLure : MonoBehaviour
{
    private FishManager fishManager;

    [SerializeField] private int playerIndex;

    private void Awake()
    {
        this.fishManager = FindObjectOfType<FishManager>();
    }

    public void AttractFishToLure()
    {
        this.fishManager.LockOntoClosestFish(transform.position, this.playerIndex);
    }
}
