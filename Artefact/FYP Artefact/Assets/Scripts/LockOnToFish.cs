using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnToFish : MonoBehaviour
{
    [SerializeField] private Transform rotatingPart;

    private void Update()
    {
        for (int i = 0; i < RippleManager.instance.ripples.Count; i++)
        {
            Debug.Log(RippleManager.instance.ripples[i].gameObject.name);
        }
    }
}
