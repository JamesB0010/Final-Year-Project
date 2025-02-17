using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    void Update()
    {
        Vector3 gyro = eteeAPI.RightDevice.gyroscope;
    }
}
