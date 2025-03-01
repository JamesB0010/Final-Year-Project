using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class LockOnToRipple : MonoBehaviour
{
    [SerializeField] private Transform rotatingPart;

    [SerializeField] private GameObject directionArrow;

    private Material directionArrowMaterial;
    [SerializeField] private float dotProductExponent;


    [SerializeField] private float timeRequiredToLockOntoFish;

    [SerializeField] private float lockOnThreashold;

    [SerializeField] private UnityEvent<float> StartedLockingOntoFish;

    [SerializeField] private UnityEvent LostLockOntoFish;

    private GameplayPipelineStage gameplayStage;

    private float startedLockOntimestamp;

    private bool lockingOn = false;
    private float bestDotProduct;
    private Ripple bestRipple;

    private void Awake()
    {
        this.gameplayStage = GetComponentInParent<GameplayPipelineStage>();
    }


    private void Start()
    {
        this.directionArrowMaterial = directionArrow.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        Vector3 forwardsDir = -rotatingPart.up;
        forwardsDir.y = 0;
        bestDotProduct = 0;
        bestRipple = null;
        
        RippleManager.VisitRipples(ripple =>
        {
            Vector3 directionToRipple = (ripple.transform.position - rotatingPart.position);
            directionToRipple.y = 0;
            directionToRipple.Normalize();
            float dot = Vector3.Dot(forwardsDir, directionToRipple);
            if (dot > bestDotProduct)
            {
                bestDotProduct = dot;
                bestRipple = ripple;
            } 
        });
           
        float mappedDotProduct = this.RemapDotProduct(bestDotProduct);
        mappedDotProduct = Mathf.Clamp01(Mathf.Pow(mappedDotProduct, this.dotProductExponent));

        if (float.IsNaN(mappedDotProduct))
            mappedDotProduct = 0f;
        
        directionArrowMaterial.SetFloat("_Alignment", mappedDotProduct);
        
        if (mappedDotProduct >= this.lockOnThreashold)
        {
            if (lockingOn == false)
            {
                this.startedLockOntimestamp = Time.timeSinceLevelLoad;
                this.lockingOn = true;
                this.StartedLockingOntoFish?.Invoke(this.timeRequiredToLockOntoFish);
            }

            if (Time.timeSinceLevelLoad - startedLockOntimestamp >= timeRequiredToLockOntoFish)
            {
                CompleteFishLockOn();
            }
        }
        else
        {
            this.lockingOn = false;
            this.LostLockOntoFish?.Invoke();
        }
    }

    private void CompleteFishLockOn()
    {
        Debug.Log("Fish locked onto!");
        this.gameplayStage.StageComplete();
        RippleManager.DestroyRipple(bestRipple);
        this.enabled = false;
        this.lockingOn = false;
        this.startedLockOntimestamp = -90000;
    }

    private float RemapDotProduct(float bestDotProduct)
    {
        return (bestDotProduct - 0.5f) / (1 - 0.5f);
    }
}
