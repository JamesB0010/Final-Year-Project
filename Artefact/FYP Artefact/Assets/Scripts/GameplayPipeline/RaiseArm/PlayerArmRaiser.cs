using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utility;

public class PlayerArmRaiser : MonoBehaviour
{
    private bool inverted;

    [SerializeField] private UnityEvent<float> ArmRaiseAmountChanged;
    
    private eteeDeviceHolder eteeDeviceHolder;

    private Animator animator;

    private Transform placeHolder;

    [SerializeField] private bool roundArmRaiseValue;

    private void Awake()
    {
        this.placeHolder = new GameObject().transform;
        this.eteeDeviceHolder = GetComponentInParent<eteeDeviceHolder>();
        this.animator = GetComponentInParent<Animator>();
        this.inverted = !this.eteeDeviceHolder.Device.isLeft;
    }

    private void Update()
    {
            Quaternion newLocalRot = eteeDeviceHolder.Device.offsetToHand * eteeDeviceHolder.Device.quaternions;
            Quaternion oldLocalRot = transform.localRotation;
            
            float interp = 1f;

            Quaternion interpLocalRot = Quaternion.identity;
            interpLocalRot.x = Mathf.Lerp(oldLocalRot.x, newLocalRot.x, interp);
            interpLocalRot.y = 0;//Mathf.Lerp(oldLocalRot.y, newLocalRot.y, interp);
            interpLocalRot.z = 0;//Mathf.Lerp(oldLocalRot.z, newLocalRot.z, interp);
            interpLocalRot.w = Mathf.Lerp(oldLocalRot.w, newLocalRot.w, interp);

            this.placeHolder.rotation = interpLocalRot;

            Debug.DrawRay(this.placeHolder.position, this.placeHolder.forward);
            Debug.DrawRay(this.placeHolder.position, Vector3.up, Color.red);
            float armRaiseAmount = Vector3.Dot(Vector3.up, this.placeHolder.forward);
            armRaiseAmount = 1 - armRaiseAmount.MapRange(-1, 1, 0, 1);
            armRaiseAmount = this.roundArmRaiseValue ? (float)Math.Round((double)armRaiseAmount, 2, MidpointRounding.AwayFromZero) : armRaiseAmount;

            if (this.inverted)
                armRaiseAmount = 1 - armRaiseAmount;

            this.ArmRaiseAmountChanged?.Invoke(armRaiseAmount);
            this.animator?.SetFloat("ArmRaiseAmount", armRaiseAmount);
    }

    public void SetAnimatorArmRaiseAmount(float amount)
    {
            this.animator.SetFloat("ArmRaiseAmount", amount);
    }
}
