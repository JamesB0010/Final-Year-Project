using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuHighScoreTextSetter : MonoBehaviour
{
    private VisualElement root;

    private void Awake()
    {
        this.root = GetComponent<UIDocument>().rootVisualElement;
    }

    private void Start()
    {
        this.root.Q<TextElement>("HighScoreText").text = PlayerPrefs.GetInt("SinglePlayerHighScore", 0).ToString();
    }
}
