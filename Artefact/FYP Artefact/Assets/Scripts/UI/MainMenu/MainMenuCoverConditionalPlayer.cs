using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainMenuCoverConditionalPlayer : MonoBehaviour
{
    private void Start()
    {
        if(MainMenuCoverSettings.MainMenuCoverTransitionEnabled)
            GetComponent<PlayableDirector>().Play();
    }
}
