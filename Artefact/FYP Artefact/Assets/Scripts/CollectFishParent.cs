using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFishParent : MonoBehaviour
{
    public void BringFishToMe(Fish fish)
    {
        fish.transform.parent = this.transform;

        fish.transform.localPosition.LerpTo(Vector3.zero, 0.8f, value =>
        {
            fish.transform.position = value;
        }, pkg =>
        {
            fish.transform.localPosition = Vector3.zero;
            //face the player
            fish.transform.rotation = new Quaternion(0, -0.5861847400665283f, 0, -0.8101774454116821f);
        }, GlobalLerpProcessor.easeInOutCurve);
    }
}
