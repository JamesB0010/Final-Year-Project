using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandGestureLocation : MonoBehaviour
{
    [SerializeField] private HandGesture handGesture;

    [SerializeField] private HandGestureReader handGestureReader;
    [SerializeField] private float idealRotatePointRotationX;
    [SerializeField] private float idealRotatePointRotationY;
    [SerializeField] private float idealRotatePointRotationZ;
    [SerializeField] private float idealRotatePointRotationW;
    
    private Quaternion idealRotatePointRotation;

    [SerializeField] private Transform rotatePoint;

    [SerializeField] private float tolerance;

    [SerializeField] private Image uiImage;

    private void Awake()
    {
        this.idealRotatePointRotation = new Quaternion(this.idealRotatePointRotationX, this.idealRotatePointRotationY,
            this.idealRotatePointRotationZ, this.idealRotatePointRotationW);
    }

    private void Update()
    {
        Debug.Log(this.rotatePoint.rotation);
        float rotationLikeness = Quaternion.Dot(this.rotatePoint.rotation, idealRotatePointRotation);

        bool rotationIsWithinTolerance = rotationLikeness >= 1 - this.tolerance && rotationLikeness <= 1 + this.tolerance;
        if (rotationIsWithinTolerance)
        {
            Debug.Log("rotation in bounds!");
            if (this.handGestureReader.CollapseToHandGesture() == this.handGesture)
            {
                this.enabled = false;
                this.uiImage.color = Color.green;
            }
        }
        Debug.Log(rotationLikeness);
    }
}
