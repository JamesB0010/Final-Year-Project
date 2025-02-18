using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSetup : MonoBehaviour
{
    [SerializeField] private GameModeHolder gameModeHolder;

    [SerializeField] private RotatePlayer player;

    private void Start()
    {
        gameModeHolder.GameMode.Setup(player);
    }
}
