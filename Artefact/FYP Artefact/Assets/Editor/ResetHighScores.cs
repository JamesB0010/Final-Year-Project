using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public static class ResetHighScores 
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
}
