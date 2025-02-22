using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class LockOnToFish : MonoBehaviour
{
    [SerializeField] private Transform rotatingPart;

    [SerializeField] private GameObject directionArrow;

    private Material directionArrowMaterial;
    [SerializeField] private float dotProductExponent;


    [SerializeField] private float timeRequiredToLockOntoFish;


    [SerializeField] private UnityEvent LockedOntoFish;

    private float startedLockOntimestamp;

    private bool lockingOn = false;

    private void Start()
    {
        this.directionArrowMaterial = directionArrow.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        Vector3 forwardsDir = -rotatingPart.up;
        forwardsDir.y = 0;
        float bestDotProduct = 0;
        Ripple bestRipple = null;
        for (int i = 0; i < RippleManager.instance.ripples.Count; i++)
        {
            Ripple ripple = RippleManager.instance.ripples[i];
            Vector3 directionToRipple = (ripple.transform.position - rotatingPart.position);
            directionToRipple.y = 0;
            directionToRipple.Normalize();
            float dot = Vector3.Dot(forwardsDir, directionToRipple);
            if (dot > bestDotProduct)
            {
                bestDotProduct = dot;
                bestRipple = ripple;
            }
        }

        float mappedDotProduct = this.RemapDotProduct(bestDotProduct);
        mappedDotProduct = Mathf.Pow(mappedDotProduct, this.dotProductExponent);
        directionArrowMaterial.SetFloat("_Alignment", mappedDotProduct);


        if (mappedDotProduct >= 0.9f)
        {
            if (lockingOn == false)
            {
                this.startedLockOntimestamp = Time.timeSinceLevelLoad;
                this.lockingOn = true;
            }

            if (Time.timeSinceLevelLoad - startedLockOntimestamp >= timeRequiredToLockOntoFish)
            {
                Debug.Log("Fish locked onto!");
                LockedOntoFish?.Invoke();
                RippleManager.instance.DestroyRipple(bestRipple);
                this.enabled = false;
            }
        }
        else
        {
            this.lockingOn = false;
        }
    }

    private float RemapDotProduct(float bestDotProduct)
    {
        return (bestDotProduct - 0.5f) / (1 - 0.5f);
    }
}
