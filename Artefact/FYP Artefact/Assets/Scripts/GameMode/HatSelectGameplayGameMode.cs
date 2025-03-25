using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HatSelectGameplayGameMode : GameMode
{
    public eteeDevice PlayerDevice { get; private set; }

    public void Initialize(eteeDevice playerDevice)
    {
        this.PlayerDevice = playerDevice;
    }
}
