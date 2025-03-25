using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class RippleWater : MonoBehaviour
{
    private Material waterMaterial;

    private static readonly int RippleOffsetPropertyId = Shader.PropertyToID("_RippleOffset");

    private List<Vector4> offsets = new List<Vector4>();
    private static readonly int RippleCount = Shader.PropertyToID("_rippleCount");

    private Dictionary<Ripple, Vector2> rippleToUvSpaceOffset = new Dictionary<Ripple, Vector2>();
    private Bounds meshBounds;

    public void OnRippleSpawned(Ripple ripple)
    {
        rippleToUvSpaceOffset.Add(ripple, this.ConvertRipplePosToUvSpaceOffset(ripple.transform.position));
    }

    private Vector2 ConvertRipplePosToUvSpaceOffset(Vector3 ripplePos)
    {
        Ray ray = new Ray(ripplePos + (Vector3.up * 1000), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
        Vector2 uvCoord = hit.textureCoord;

        float remappedX = uvCoord.x.MapRange(0, 1, 0.5f, -0.5f);
        float remappedY = uvCoord.y.MapRange(0, 1, 0.5f, -0.5f);
        return new Vector2(remappedX, remappedY);
        }

        return Vector2.zero;
    }

    public void OnRippleDestroyed(Ripple ripple)
    {
        rippleToUvSpaceOffset.Remove(ripple);
    }

    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        this.waterMaterial = meshRenderer.material;
        meshBounds = meshRenderer.bounds;
    }

    public void OnGameSetup()
    {
        this.waterMaterial.SetVectorArray(RippleOffsetPropertyId, new Vector4[GameplayGameMode.CurrentGameplayGameMode.MaxRippleCount]);
    }

    private void Update()
    {
        this.UpdateRippleToUvSpaceVals();
        offsets.Clear();
        foreach (Vector2 rippleOffset in this.rippleToUvSpaceOffset.Values)
        {
            offsets.Add(new Vector4(rippleOffset.x, rippleOffset.y, 0,0 ));
        }
        
        this.waterMaterial.SetInt(RippleCount, offsets.Count);
        
        if(offsets.Count == 0)
            return;
        
        this.waterMaterial.SetVectorArray(RippleOffsetPropertyId, offsets.ToArray());
    }

    private void UpdateRippleToUvSpaceVals()
    {
        List<Tuple<Ripple, Vector2>> valuesToUpdate = new List<Tuple<Ripple, Vector2>>();
        foreach (Ripple ripple in this.rippleToUvSpaceOffset.Keys)
        {
            valuesToUpdate.Add(new Tuple<Ripple, Vector2>(ripple, this.ConvertRipplePosToUvSpaceOffset(ripple.transform.position)));
        }
        
        foreach (Tuple<Ripple,Vector2> tuple in valuesToUpdate)
        {
            this.rippleToUvSpaceOffset[tuple.Item1] = tuple.Item2;
        }
    }
}
