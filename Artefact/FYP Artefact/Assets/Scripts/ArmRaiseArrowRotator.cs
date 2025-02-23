using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRaiseArrowRotator : MonoBehaviour
{
    [SerializeField] private float minRot;

    [SerializeField] private float maxRot;

    [SerializeField] private ArmRaiseBox[] boxes;

    private int currentlyFillingBox = -1;

    public void ArmRaisedValueChanged(float newVal)
    {
        transform.rotation = Quaternion.Euler(0,0,Mathf.Lerp(minRot, maxRot, newVal));
        this.StopFillingAllBoxes();
        
        if (newVal >= 0 && newVal < 0.2)
        {
            if (this.currentlyFillingBox != 0)
            {
                this.currentlyFillingBox = 0;
                this.boxes[currentlyFillingBox].StartFilling();
                Debug.Log("First box");
            }
        }

        if (newVal >= 0.3 && newVal < 0.4)
        {
            if (this.currentlyFillingBox != 1)
            {
                this.currentlyFillingBox = 1;
                this.boxes[currentlyFillingBox].StartFilling();
                Debug.Log("Second box");
            }
        }

        if (newVal >= 0.4 && newVal < 0.6)
        {
            if (this.currentlyFillingBox != 2)
            {
                this.currentlyFillingBox = 2;
                this.boxes[currentlyFillingBox].StartFilling();
                Debug.Log("third box");
            }
        }

        if (newVal >= 0.6 && newVal < 0.8)
        {
            if (this.currentlyFillingBox != 3)
            {
                this.currentlyFillingBox = 3;
                this.boxes[currentlyFillingBox].StartFilling();
                Debug.Log("fouth box");
            }
        }

        if (newVal > 0.8)
        {
            if (this.currentlyFillingBox != 4)
            {
                this.currentlyFillingBox = 4;
                this.boxes[currentlyFillingBox].StartFilling();
                Debug.Log("Fifth box");
            }
        }
    }

    private void StopFillingAllBoxes()
    {
        foreach (ArmRaiseBox box in boxes)
        {
            box.StopFilling();
        }
    }
}
