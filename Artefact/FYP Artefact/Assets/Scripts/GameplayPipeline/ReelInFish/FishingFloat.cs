using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingFloat : MonoBehaviour
{
    private Fish attachedFish;

    private float yPos;

    private void Awake()
    {
        this.yPos = transform.position.y;
        base.gameObject.SetActive(false);
    }

    public void AttachToFish(Fish fish)
    {
        this.attachedFish = fish;
        base.gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position = this.attachedFish.transform.position;
        transform.position = new Vector3(transform.position.x, this.yPos, this.transform.position.z);
    }
}
