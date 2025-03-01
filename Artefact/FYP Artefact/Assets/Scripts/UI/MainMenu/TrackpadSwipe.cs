using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace eteeDeviceInput
{
    public class TrackpadSwipe
    {
        public event Action up, down;

        public TrackpadSwipe(eteeDevice device)
        {
            this.device = device;
        }

        private eteeDevice device;

        private bool readyX = true;
        private bool readyY = true;

        public void Update()
        {
            Vector2 input = this.device.trackpadCoordinates;

            float processedYInput = this.Process1DInput(input.y, ref this.readyY);
            if (processedYInput == 1)
                this.up?.Invoke();
            else if (processedYInput == -1)
                this.down?.Invoke();
        }

        private float Process1DInput(float input, ref bool readyState)
        {
            float normalizedInput = ((input - 126).MapRange(0, 252, 0, 1) * 2);
            if (normalizedInput >= 0.5f)
            {
                if (readyState)
                {
                    readyState = false;
                    return 1f;
                }
            }

            if (normalizedInput <= -0.5f)
            {
                if (readyState)
                {
                    readyState = false;
                    return -1f;
                }
            }

            if (normalizedInput == 0)
                readyState = true;

            return 0;
        }
    }
}