using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eteeApiSettings : ScriptableObject 
{
    [SerializeField] private bool apiEnabled;
    
    public bool ApiEnabled
    {
        get => this.apiEnabled;
        set => this.apiEnabled = value;
    }
}
