using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleSpawnerDebugVis : MonoBehaviour
{
    private Mesh cubeMeshForVisualisation;

    private void Awake()
    {
        this.InitCubeMeshForVis();
    }

    private void InitCubeMeshForVis()
    {
        var cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        this.cubeMeshForVisualisation = cubeObj.GetComponent<MeshFilter>().sharedMesh;
        if (Application.isPlaying)
            Destroy(cubeObj);
        else
            DestroyImmediate(cubeObj);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireMesh(cubeMeshForVisualisation, transform.position, transform.rotation, transform.localScale);
    }
}