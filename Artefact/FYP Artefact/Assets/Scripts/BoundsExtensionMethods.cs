using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoundsExtensionMethods 
{
    public static Vector3 RandomXZInBounds(this Bounds bounds)
    {
        float randX = Random.Range(bounds.min.x, bounds.max.x);
        float randZ = Random.Range(bounds.min.z, bounds.max.z);
                
        return new Vector3(randX, 0, randZ);
    }
}
