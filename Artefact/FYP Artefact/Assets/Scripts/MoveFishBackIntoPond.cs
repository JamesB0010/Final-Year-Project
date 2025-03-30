using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFishBackIntoPond : MonoBehaviour
{
    private FishManager fishManager;

    private void Awake()
    {
        this.fishManager = FindObjectOfType<FishManager>();
    }

    public void Execute()
    {
        this.fishManager.PlaceFishBackIntoPond(transform.GetChild(0));
    }
}
