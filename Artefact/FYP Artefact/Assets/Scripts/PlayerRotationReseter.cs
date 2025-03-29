using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationReseter : MonoBehaviour
{
    [SerializeField] private Transform playerRoot;
    public void ResetPlayerRotation()
    {
        this.playerRoot.localRotation = Quaternion.identity;
    }
}
