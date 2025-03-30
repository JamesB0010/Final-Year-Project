using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CollectFishParent : MonoBehaviour
{
    [SerializeField] private Transform[] cutsceneFishModels;
    public void EnableDopplegangerFishForCutscene(Fish fish)
    {
        GameObject doppelganger = cutsceneFishModels[(int)fish.fishType].gameObject;
        doppelganger.SetActive(true);
    }
}
