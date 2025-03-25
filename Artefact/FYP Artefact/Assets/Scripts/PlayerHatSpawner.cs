using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHatSpawner : MonoBehaviour
{
    [SerializeField] private Transform headBone;

    [SerializeField] private HatDatabase hatDatabase;

    [SerializeField] private PlayerHatDataHolder hatData;

    [SerializeField] private GameModeHolder gameModeHolder;

    [SerializeField] private bool player1;

    private void Start()
    {
        switch (gameModeHolder.GameplayGameMode)
        {
            case MultiplayerGameplayGameMode multiplayerGameplayGameMode:
                if (this.player1)
                {
                    GameObject hatPrefab = hatDatabase.GetHat(hatData.leftControllerHatIndex);
                    if(hatPrefab == null)
                        return;
                    Instantiate(hatPrefab, this.headBone);
                }
                else
                {
                    GameObject hatPrefab = hatDatabase.GetHat(hatData.rightControllerHatIndex);
                    if (hatPrefab == null)
                        return;
                    Instantiate(hatPrefab, this.headBone);
                }

                break;
            case SinglePlayerGameplayGameMode singlePlayerGameplayGameMode:
                if (singlePlayerGameplayGameMode.PlayerDevice.isLeft)
                {
                    GameObject hatPrefab = hatDatabase.GetHat(this.hatData.leftControllerHatIndex);
                    if (hatPrefab == null)
                        return;
                    Instantiate(hatPrefab, this.headBone);
                }
                else
                {
                    GameObject hatPrefab = hatDatabase.GetHat(this.hatData.rightControllerHatIndex);
                    if (hatPrefab == null)
                        return;
                    Instantiate(hatPrefab, this.headBone);
                }
                break;
            default:
                return;
        }
    }
}
