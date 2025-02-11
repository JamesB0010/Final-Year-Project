using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eteeApiSettings : ScriptableObject 
{
    [SerializeField] private bool apiEnabled;
    
    public bool ApiEnabled
    {
        get => this.apiEnabled;
        set
        {
            bool noChangeDetected = this.apiEnabled == value;
            if (noChangeDetected)
                return;
            
            this.apiEnabled = value;

            if (this.apiEnabled == true)
            {
                Debug.Log("etee api enabled");
            }
            else
            {
                Debug.Log("etee api disabled");
            }
        }
    }
}
