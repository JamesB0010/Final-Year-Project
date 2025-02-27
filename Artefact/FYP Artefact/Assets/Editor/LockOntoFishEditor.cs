using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LockOnToRipple))]
public class LockOntoFishEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Lock On"))
        {
            LockOnToRipple t = target as LockOnToRipple;
            
            t.LockOnToFish_Debug();
        }
    }
}
