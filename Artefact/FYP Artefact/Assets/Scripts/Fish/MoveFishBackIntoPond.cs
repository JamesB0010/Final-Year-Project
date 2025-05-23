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

    public void Execute(Fish fish)
    {
        fish.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        fish.overrideSteeringSettings.active = false;
        this.fishManager.PlaceFishBackIntoPond(fish.transform);
    }
}
