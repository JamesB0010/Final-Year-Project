using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHatDataPersistance : MonoBehaviour
{
    [SerializeField] private PlayerHatDataHolder hatData;
    void Start()
    {
        hatData.leftControllerHatIndex = PlayerPrefs.GetInt("leftControllerHatIndex", -1);
        hatData.rightControllerHatIndex = PlayerPrefs.GetInt("rightControllerHatIndex", -1);
    }
}
