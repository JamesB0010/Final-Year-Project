using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class PlayerHatDataHolder : ScriptableObject
{
    public int leftControllerHatIndex;

    public int rightControllerHatIndex;

    public void SetLeftControllerHatIndex(int index)
    {
        this.leftControllerHatIndex = index;
    }

    public void SetRightControllerHatIndex(int index)
    {
        this.rightControllerHatIndex = index;
    }

    public void Reset()
    {
        this.leftControllerHatIndex = 0;
        this.rightControllerHatIndex = 0;
    }
}
