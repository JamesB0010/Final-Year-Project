using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FishStartPositions2DArray
{
    public TransformArray[] transformArrays;
}


[Serializable]
public class TransformArray
{
    public Transform[] transforms;
}