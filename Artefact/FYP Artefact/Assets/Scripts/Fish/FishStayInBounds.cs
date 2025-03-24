using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishStayInBounds : FishSteeringBehaviour
{
    private Vector3 lastBoundryHitPosition;
    [SerializeField] private float maxSteeringForce = 2000f; // Max steering force
    [SerializeField] private float boundaryInfluenceDistance = 10f; // Distance at which the boundary starts influencing movement

    [SerializeField] private float invExponent;
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, LayerMask.GetMask("FishBoundary")))
        {
            this.lastBoundryHitPosition = hitInfo.point;

            base.velocity = ((transform.position - this.lastBoundryHitPosition).normalized) * this.CalculateMagnitude(hitInfo.distance);
        }
    }

    //gpt
    private float CalculateMagnitude(float distance)
    {
        float normalizedDistance = Mathf.Clamp01(distance / boundaryInfluenceDistance); // Normalize between 0 and 1
        float inverseFalloff = Mathf.Lerp(maxSteeringForce, 0f, normalizedDistance); // Smooth transition
        return inverseFalloff;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.lastBoundryHitPosition, 1);
    }
}
