using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu]
public class HandGestureProfile : ScriptableObject
{
    public Vector4 pullAmounts;


    public float GetPoseLikeness(float index, float middle, float ring, float pinky)
    {
        float indexDifference = Mathf.Abs(pullAmounts.x - index);
        float middleDifference = Mathf.Abs(pullAmounts.y - middle);
        float ringDifference = Mathf.Abs(pullAmounts.z - ring);
        float pinkyDifference = Mathf.Abs(pullAmounts.w - pinky);

        float totalDifference = indexDifference + middleDifference + ringDifference + pinkyDifference;

        float likeness = 1 - (totalDifference / (4 * 150));
        return likeness;
    }
}
