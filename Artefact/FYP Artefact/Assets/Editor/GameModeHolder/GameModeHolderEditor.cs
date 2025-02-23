using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Properties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameModeHolder))]
public class GameModeHolderEditor : Editor
{
    [SerializeField] private VisualTreeAsset visualTreeAsset;

    private GameModeHolder gameModeHolder;

    private Dictionary<string, GameMode> gameModes;
    private VisualElement root;

    private InspectorElement currentGameModeInspector;

    public override VisualElement CreateInspectorGUI()
    {
        this.gameModeHolder = target as GameModeHolder;

        var _ = this.gameModeHolder.GameMode;
        
        root = this.visualTreeAsset.CloneTree();

        this.gameModes = new Dictionary<string, GameMode>();

        this.currentGameModeInspector = null;
        this.SetDropDownCurrentValueText();

        this.DiscoverGameModeNames();

        this.PopulateDropDownListOptions();

        this.ListenToDropdownEvents();

        this.DrawCurrentGameModeInspector();
        
        return root;
    }

    private void DrawCurrentGameModeInspector()
    {
        var inspectorParent = root.Q<VisualElement>("InfoAboutGameMode");
        if(this.currentGameModeInspector != null)
            inspectorParent.Remove(this.currentGameModeInspector);
        
        this.currentGameModeInspector = new InspectorElement(this.gameModeHolder.GameMode);
        inspectorParent.Add(this.currentGameModeInspector);
    }


    private void SetDropDownCurrentValueText()
    {
        root.Q<DropdownField>("CurrentSelectedGameModeDropdown").value = this.gameModeHolder.GameMode.name;
    }
    private void DiscoverGameModeNames()
    {
        var gameModeGuids = AssetDatabase.FindAssets("t:GameMode");

        foreach (string guid in gameModeGuids)
        {
            var gameMode = AssetDatabase.LoadAssetAtPath<GameMode>(AssetDatabase.GUIDToAssetPath(guid));
            this.gameModes.Add(gameMode.name, gameMode);
        }
    }
    private void PopulateDropDownListOptions()
    {
        DropdownField dropdownField = root.Q<DropdownField>("CurrentSelectedGameModeDropdown");
        dropdownField.choices = this.gameModes.Keys.ToList();
    }
    private void ListenToDropdownEvents()
    {
        DropdownField dropdownField = root.Q<DropdownField>("CurrentSelectedGameModeDropdown");
        dropdownField.RegisterCallback<ChangeEvent<string>>(this.SelectedGameModeDropdownChanged);
    }

    private void SelectedGameModeDropdownChanged(ChangeEvent<string> evt)
    {
        this.gameModeHolder.GameMode = this.gameModes[evt.newValue];
        this.DrawCurrentGameModeInspector();
    }
}
