using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerGameMode : GameMode
{
    public SinglePlayerGameMode(eteeDevice playerDevice)
    {
        this.PlayerDevice = playerDevice;
    }
    public eteeDevice PlayerDevice { get; private set; }
    
    
    public override void Setup(RotatePlayer player)
    {
        player.Device = this.PlayerDevice;
    }
}
