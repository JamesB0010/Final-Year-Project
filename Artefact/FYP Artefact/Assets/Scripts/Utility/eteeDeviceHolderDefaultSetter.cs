using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eteeDeviceHolderDefaultSetter : MonoBehaviour
{
    [SerializeField] private int controllerIndex;

    private void Awake()
    {
        eteeDevice device = controllerIndex == 0 ? eteeAPI.LeftDevice : eteeAPI.RightDevice;
        GetComponent<eteeDeviceHolder>().Device = device;
    }
}
