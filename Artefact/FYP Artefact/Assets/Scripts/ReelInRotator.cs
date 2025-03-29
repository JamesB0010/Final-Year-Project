using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelInRotator : MonoBehaviour
{
    private Quaternion yRotateFrom;

    [SerializeField] private float yRotateToX;
    [SerializeField] private float yRotateToY;
    [SerializeField] private float yRotateToZ;
    [SerializeField] private float yRotateToW;
    
    private Quaternion yRotateTo;

    [SerializeField] private float rotationTime;

    private void Awake()
    {
        this.yRotateFrom = transform.rotation;
        yRotateTo = new Quaternion(this.yRotateToX, yRotateToY, yRotateToZ, yRotateToW);
    }

    public void Activate()
    {
        if (!base.enabled)
            return;
        
        this.yRotateFrom.LerpTo(this.yRotateTo, this.rotationTime, value =>
        {
            transform.rotation = value;
        }, pkg =>
        {
            pkg.Reverse();
            GlobalLerpProcessor.AddLerpPackage(pkg);
        });
    }

    private void Update()
    {
    }
}
