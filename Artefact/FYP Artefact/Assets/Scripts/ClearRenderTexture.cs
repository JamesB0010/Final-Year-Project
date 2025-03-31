using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ClearRenderTexture : MonoBehaviour
{
    [SerializeField] public RenderTexture renderTexture;

    [SerializeField] private Camera camera;

    private void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += this.ClearTexture;
    }

    private void OnDisable(){
        RenderPipelineManager.endCameraRendering -= this.ClearTexture;
    }

    private void ClearTexture(ScriptableRenderContext ctx, Camera cam)
    {
        if (cam != camera)
            return;
        
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = this.renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
    }
}
