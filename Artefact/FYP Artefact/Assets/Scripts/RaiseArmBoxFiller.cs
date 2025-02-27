using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaiseArmBoxFiller : MonoBehaviour
{
    [SerializeField] private ArmRaiseBox[] boxes;

    private GameplayPipelineStage gameplayPipelineStage;

    private int currentlyFillingBox = 4;

    private int filledBoxes;

    private void Awake()
    {
        this.gameplayPipelineStage = GetComponentInParent<GameplayPipelineStage>();
    }

    public void ArmRaisedValueChanged(float newVal)
    {
        if (newVal >= 0 && newVal < 0.2)
        {
            if (this.currentlyFillingBox != 0)
            {
                this.boxes[currentlyFillingBox].StopFilling();
                this.currentlyFillingBox = 0;
                this.boxes[currentlyFillingBox].StartFilling();
            }
        }

        if (newVal >= 0.3 && newVal < 0.4)
        {
            if (this.currentlyFillingBox != 1)
            {
                this.boxes[currentlyFillingBox].StopFilling();
                this.currentlyFillingBox = 1;
                this.boxes[currentlyFillingBox].StartFilling();
            }
        }

        if (newVal >= 0.4 && newVal < 0.6)
        {
            if (this.currentlyFillingBox != 2)
            {
                this.boxes[currentlyFillingBox].StopFilling();
                this.currentlyFillingBox = 2;
                this.boxes[currentlyFillingBox].StartFilling();
            }
        }

        if (newVal >= 0.6 && newVal < 0.8)
        {
            if (this.currentlyFillingBox != 3)
            {
                this.boxes[currentlyFillingBox].StopFilling();
                this.currentlyFillingBox = 3;
                this.boxes[currentlyFillingBox].StartFilling();
            }
        }

        if (newVal > 0.8)
        {
            if (this.currentlyFillingBox != 4)
            {
                this.boxes[currentlyFillingBox].StopFilling();
                this.currentlyFillingBox = 4;
                this.boxes[currentlyFillingBox].StartFilling();
            }
        }
    }

    public void BoxFilled()
    {
        this.filledBoxes++;

        if (this.filledBoxes == this.boxes.Length)
        {
            this.gameplayPipelineStage.StageComplete();
            Debug.Log("Boxes Full");
        }
    }
}
