using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerArmRaiser : MonoBehaviour
{
    [SerializeField] private float delayBeforeStartup;

    [SerializeField] private Transform arm;

    private Quaternion armRotationStart;

    [SerializeField] private Quaternion armRotationEnd;

    private eteeDeviceHolder eteeDeviceHolder;

    private void Start()
    {
        this.eteeDeviceHolder = GetComponent<eteeDeviceHolder>();
        this.armRotationStart = this.arm.localRotation;
    }

    public void EnteredArmRaisePhase()
    {
        StartCoroutine(nameof(StartUpDelayed));

        this.Startup();
    }

    private void Startup()
    {
        this.enabled = true;
        this.arm.localRotation= this.armRotationStart;
    }

    private IEnumerator StartUpDelayed()
    {
        yield return new WaitForSeconds(delayBeforeStartup);
    }

    private void Update()
    {
        
    }
}
