using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerArmRaiser : MonoBehaviour
{
    [SerializeField] private float delayBeforeStartup;

    [SerializeField] private Transform arm;

    private eteeDeviceHolder eteeDeviceHolder;

    private Animator animator;

    [SerializeField] private float armRaiseOffset;

    [SerializeField] private UnityEvent StartArmRaiseGameSection;

    [SerializeField] private UnityEvent<float> ArmRaiseAmountChanged;

    private void Awake()
    {
        this.eteeDeviceHolder = GetComponent<eteeDeviceHolder>();
        this.animator = GetComponent<Animator>();
    }
    public void EnteredArmRaisePhase()
    {
        StartCoroutine(nameof(StartUpDelayed));

    }
    private IEnumerator StartUpDelayed()
    {
        yield return new WaitForSeconds(delayBeforeStartup);
        this.Startup();
    }

    private void Startup()
    {
        this.enabled = true;
        this.StartArmRaiseGameSection?.Invoke();
    }
    
    private void Update()
    {
            Quaternion newLocalRot = eteeDeviceHolder.Device.offsetToHand * eteeDeviceHolder.Device.quaternions;
            Quaternion oldLocalRot = transform.localRotation;
            float interp = 0.2f;

            Quaternion interpLocalRot = Quaternion.identity;
            interpLocalRot.x = Mathf.Lerp(oldLocalRot.x, newLocalRot.x, interp);
            interpLocalRot.y = Mathf.Lerp(oldLocalRot.y, newLocalRot.y, interp);
            interpLocalRot.z = Mathf.Lerp(oldLocalRot.z, newLocalRot.z, interp);
            interpLocalRot.w = Mathf.Lerp(oldLocalRot.w, newLocalRot.w, interp);
            
            float armRaiseAmount = Mathf.Clamp01(1 - (interpLocalRot.x * -5 + this.armRaiseOffset));

            this.ArmRaiseAmountChanged?.Invoke(armRaiseAmount);
            this.animator.SetFloat("ArmRaiseAmount", armRaiseAmount);
    }
}
