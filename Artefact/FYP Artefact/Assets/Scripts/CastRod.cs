using UnityEngine;
using UnityEngine.Events;

public class CastRod : MonoBehaviour
{
    private bool readyToRaise;

    private bool readyToCast;

    [SerializeField] private UnityEvent PrimeToCastRod;

    [SerializeField] private UnityEvent CastRodEvent;

    [SerializeField] private UnityEvent FinshedCastRodEvent;

    public void SetReadyToCast(bool value)
    {
        this.readyToRaise = value;
    }

    public void ArmRaisedValueChanged(float value)
    {
        if (!this.readyToRaise)
            return;


        if (value == 1 && this.readyToCast == false)
        {
            this.PrimeToCastRod?.Invoke();
            this.readyToCast = true;
        }

        if (value <= 0.6)
        {
            this.CastRodEvent?.Invoke();
            this.readyToRaise = false;
            this.readyToCast = false;
            Animator animator = GetComponentInParent<Animator>();
            animator.GetFloat("ArmRaiseAmount").LerpTo(0f, 0.3f, val => animator.SetFloat("ArmRaiseAmount", val), pkg =>
            {
                this.FinshedCastRodEvent?.Invoke();
            });
        }
    }
}
