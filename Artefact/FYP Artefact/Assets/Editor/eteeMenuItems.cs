using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class eteeMenuItems 
{
    [MenuItem("etee/EnableApi")]
    public static void EnableApi()
    {
        eteeApiSettings settings = Resources.Load<eteeApiSettings>("eteeApiSettings/eteeApiSettings");
        settings.ApiEnabled = true;
        EditorUtility.SetDirty(settings);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("etee/DisableApi")]
    public static void DisableApi()
    {
        eteeApiSettings settings = Resources.Load<eteeApiSettings>("eteeApiSettings/eteeApiSettings");
        settings.ApiEnabled = false;
        EditorUtility.SetDirty(settings);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
