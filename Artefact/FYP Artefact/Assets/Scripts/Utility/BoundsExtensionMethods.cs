using UnityEngine;

namespace Utility
{
    public static class BoundsExtensionMethods 
    {
        public static Vector3 RandomXZInBounds(this Bounds bounds)
        {
            float randX = Random.Range(bounds.min.x, bounds.max.x);
            float randY = Random.Range(bounds.min.y, bounds.max.y);
            float randZ = Random.Range(bounds.min.z, bounds.max.z);
                
            return new Vector3(randX, randY, randZ);
        }
    }
}
