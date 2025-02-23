using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LockOnToFish))]
public class LockOntoFishEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Lock On"))
        {
            LockOnToFish t = target as LockOnToFish;
            
            t.LockOnToFish_Debug();
        }
    }
}
