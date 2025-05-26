using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchFishGetCaught : GameplayPipelineStage
{
    private Vector3 lastRipplePos;

    [SerializeField] private Transform virtualCamParent;
    
    public void OnRippleLockOnFinised(Vector3 ripplePos)
    {
        this.lastRipplePos = ripplePos;
    }

    public override void StageComplete()
    {
        base.StageComplete();
    }

    public override void StageEntered()
    {
        base.StageEntered();

        this.virtualCamParent.position = this.lastRipplePos;
        Debug.Log(this.lastRipplePos);
    }
}
