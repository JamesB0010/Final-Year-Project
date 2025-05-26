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

    [SerializeField] private QuaternionLerpPackage lerpPackage;

    private void Awake()
    {
        this.yRotateFrom = transform.rotation;
        yRotateTo = new Quaternion(this.yRotateToX, yRotateToY, yRotateToZ, yRotateToW);
    }

    public void Activate()
    {
        this.lerpPackage = this.yRotateFrom.LerpTo(this.yRotateTo, this.rotationTime, value =>
        {
            transform.rotation = value;
        }, pkg =>
        {
            pkg.Reverse();
            GlobalLerpProcessor.AddLerpPackage(pkg);
        });
    }

    public void Deactivate()
    {
        GlobalLerpProcessor.RemovePackage(this.lerpPackage);

        transform.rotation = this.yRotateFrom;
    }
}
