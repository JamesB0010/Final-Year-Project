using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class FishWander : FishSteeringBehaviour
{
    [Serializable]
    struct WanderPoint
    {
        public float radLat;
        public float radLong;
        public float displacementRange;

        public Vector3 ToSpherePoint(Transform referenceTransform, Vector3 center, float radius)
        {
            float xPos = radius * Mathf.Cos(radLat) * Mathf.Cos(radLong);
            float yPos = radius * Mathf.Cos(radLat) * Mathf.Sin(radLong);
            float zPos = radius * Mathf.Sin(radLat);
            
            Vector3 localPoint = new Vector3(xPos, yPos, zPos);

            return center + referenceTransform.rotation * localPoint;
        }

        public void AddRandomDisplacement()
        {
            this.radLat += Random.Range(-this.displacementRange, this.displacementRange);
            this.radLong += Random.Range(-this.displacementRange, this.displacementRange); 
        }
    }
    
    private Rigidbody rigidbody;
    private Vector3 wanderCenterPoint;
    [SerializeField] private float lookAheadDistance;
    [SerializeField] private float speed;

    [SerializeField] private float wanderRadius;

    [SerializeField] private WanderPoint targetWanderPoint;

    [SerializeField] private float wanderStrength;

    [SerializeField] private float zRotSpeed;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        base.velocity = this.rigidbody.velocity;
        wanderCenterPoint = this.CalculateWanderPoint();
        Vector3 targetPointPos = this.targetWanderPoint.ToSpherePoint(transform, this.wanderCenterPoint, this.wanderRadius);
        Vector3 steeringForce = (targetPointPos - transform.position).normalized * this.wanderStrength;
        base.velocity += steeringForce;
        base.velocity = base.velocity.normalized * Mathf.Clamp(base.velocity.magnitude, 0, this.speed);

        Vector3 up = Vector3.Cross(this.velocity.normalized, transform.right);
        transform.LookAt(targetPointPos, up);

        Vector3 rot = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rot.x, rot.y, 0),
            Time.deltaTime * this.zRotSpeed);

        this.targetWanderPoint.AddRandomDisplacement();
    }

    private Vector3 CalculateWanderPoint()
    {
        Vector3 dir = base.velocity.normalized;
        Vector3 wanderDistance = dir * this.lookAheadDistance;
        return transform.position + wanderDistance;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(this.wanderCenterPoint, 1f);
        
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.targetWanderPoint.ToSpherePoint(transform, this.wanderCenterPoint, this.wanderRadius), 0.3f);
    }
}
