using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HatChooser))]
public class HatChooserCustomEditor : Editor
{
    private HatChooser t;

    private int hatToEquip = 0;
    private void OnEnable()
    {
        this.t = base.target as HatChooser;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!Application.isPlaying)
            return;

        this.hatToEquip = EditorGUILayout.IntField("Hat to equip", this.hatToEquip);

        if (GUILayout.Button("Equip Hat"))
        {
            this.t.EquipHat(this.hatToEquip);
        }
    }
}
