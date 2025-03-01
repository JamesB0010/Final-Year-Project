using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MainMenuCoverSettings
{
    private static bool mainMenuCoverTransitionEnabled = false;

    public static bool MainMenuCoverTransitionEnabled => mainMenuCoverTransitionEnabled;
    
    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        SceneManager.sceneLoaded += SceneChanged;
    }

    private static void SceneChanged(Scene arg0, LoadSceneMode arg1)
    {
        mainMenuCoverTransitionEnabled = true;

        SceneManager.sceneLoaded -= SceneChanged;
    }
}
