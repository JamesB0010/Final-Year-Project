using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CastRod : MonoBehaviour
{
    [SerializeField] private UnityEvent ReadyToCast;
    
    private bool readyToCast;

    private GameplayPipelineStage gameplayPipelineStage;

    private void Awake()
    {
        this.gameplayPipelineStage = GetComponentInParent<GameplayPipelineStage>();
    }

    public void ArmRaisedValueChanged(float value)
    {
        bool armFullyRaisedFirstTime = value == 1 && this.readyToCast == false;
        if (armFullyRaisedFirstTime)
        {
            this.readyToCast = true;
            this.ReadyToCast?.Invoke();
        }

        if (value <= 0.6)
        {
            AnimateRodBackDown();
        }
    }

    public void SetReadyToCast(bool value)
    {
        this.readyToCast = value;
    }

    private void AnimateRodBackDown()
    {
        Animator animator = GetComponentInParent<Animator>();
        animator.GetFloat("ArmRaiseAmount").LerpTo(0f, 0.3f, val => animator.SetFloat("ArmRaiseAmount", val),
            pkg => this.gameplayPipelineStage.StageComplete());
    }
}
