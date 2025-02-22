using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RippleManager : MonoBehaviour
{
    public List<Ripple> ripples { get; set; }
    
    public static RippleManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            instance = null;
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        this.ripples = new List<Ripple>();
    }

    public void NewRipple(Ripple ripple)
    {
        this.ripples.Add(ripple);
    }

    private void OnDestroy()
    {
        instance = null;
        Destroy(this.gameObject);
    }
}
