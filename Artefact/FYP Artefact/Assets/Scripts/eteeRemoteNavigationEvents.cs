using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eteeRemoteNavigationEvents
{
    public event Action left, right, up, down;
    public eteeRemoteNavigationEvents(eteeDevice device)
    {
        this.device = device;
    }
    
    private eteeDevice device;

    private bool ready = true;
    public void Update()
    {
        Vector2 input = this.device.trackpadCoordinates;

        float processedXInput = this.Process1DInput(input.x);
        
        if (processedXInput == 1)
            this.right?.Invoke();
        else if (processedXInput == -1)
            this.left?.Invoke();

        float processedYInput = this.Process1DInput(input.y);
        if (processedYInput == 1)
            this.up?.Invoke();
        else if (processedYInput == -1)
            this.down?.Invoke();
    }
    private float Process1DInput(float input)
    {
        float normalizedInput = ((input - 126).MapRange(0, 252, 0, 1) * 2);
        if (normalizedInput >= 0.5f)
        {
            if (this.ready)
            {
                this.ready = false;
                return 1f;
            }
        }

        if (normalizedInput <= -0.5f)
        {
            if (this.ready)
            {
                this.ready = false;
                return -1f;
            }
        }

        if (normalizedInput == 0)
            this.ready = true;

        return 0;
    }
}
