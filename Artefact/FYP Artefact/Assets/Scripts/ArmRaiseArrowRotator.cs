using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmRaiseArrowRotator : MonoBehaviour
{
    [SerializeField] private float minRot;

    [SerializeField] private float maxRot;

    public void ArmRaisedValueChanged(float newVal)
    {
        transform.rotation = Quaternion.Euler(0,0,Mathf.Lerp(minRot, maxRot, newVal));
    }
}
