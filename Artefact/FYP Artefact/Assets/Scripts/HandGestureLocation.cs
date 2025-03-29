using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    [SerializeField] private UnityEvent finished;
    
    private int progress = 0;
    private int maxProgress = 3;

    private bool entered;

    private int Progress
    {
        get => this.progress;
        set
        {
            this.progress = value;

            if (this.progress == this.maxProgress)
            {
                this.enabled = false;
                this.finished?.Invoke();
            }

            float t = ((float)this.progress) / this.maxProgress;
            this.uiImage.color = Color.Lerp(Color.red , Color.green, t);
        }
    }

    private void Awake()
    {
        this.idealRotatePointRotation = new Quaternion(this.idealRotatePointRotationX, this.idealRotatePointRotationY,
            this.idealRotatePointRotationZ, this.idealRotatePointRotationW);
    }

    private void Update()
    {
        //Debug.Log(this.rotatePoint.rotation);
        float rotationLikeness = Quaternion.Dot(this.rotatePoint.rotation, idealRotatePointRotation);

        bool rotationIsWithinTolerance = rotationLikeness >= 1 - this.tolerance && rotationLikeness <= 1 + this.tolerance;
        if (rotationIsWithinTolerance)
        {
            if (this.entered)
                return;
            
            this.entered = true;
            if (this.handGestureReader.CollapseToHandGesture() == this.handGesture)
            {
                this.Progress++;
            }
        }
        else
        {
            this.entered = false;
        }
        //Debug.Log(rotationLikeness);
    }
}
