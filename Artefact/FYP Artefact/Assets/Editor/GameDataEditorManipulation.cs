using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public static class GameDataEditorManipulation 
{
    [MenuItem("GameData/ResetHighScorees")]
    public static void DoResetHighScores()
    {
        ResetSinglePlayerScore();
    }

    private static void ResetSinglePlayerScore()
    {
        PlayerPrefs.SetInt("SinglePlayerHighScore", 0);
    }

    [MenuItem("GameData/ResetHats")]
    public static void ResetHats()
    {
        PlayerHatDataHolder hatDatHolder = Resources.Load<PlayerHatDataHolder>("PlayerHatData");
        hatDatHolder.Reset();
        EditorUtility.SetDirty(hatDatHolder);
        AssetDatabase.SaveAssetIfDirty(hatDatHolder);
    }
}
