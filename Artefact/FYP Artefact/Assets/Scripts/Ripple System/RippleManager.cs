using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RippleManager : MonoBehaviour
{
    public UnityEvent<Ripple> rippleSpawned;

    public UnityEvent<Ripple> rippleDestroyed;
    private static RippleManager instance { get; set; }

    private List<Ripple> ripples = new List<Ripple>();
    
    public static int MaxRipples;
    public static bool RipplesFull => instance.ripples.Count == MaxRipples;

    public static void VisitRipples(Action<Ripple> action)
    {
        foreach (Ripple ripple in instance.ripples)
        {
            action(ripple);
        }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            instance = null;
            Destroy(this.gameObject);
        }
    }

    public static void RegisterNewRipple(Ripple ripple)
    {
        instance.ripples.Add(ripple);
        instance.rippleSpawned?.Invoke(ripple);
    }
        
    
    private void OnDestroy()
    {
        //Clean up
        instance = null;
        Destroy(this.gameObject);
    }

    public static void DestroyRipple(Ripple ripple)
    {
        instance.rippleDestroyed?.Invoke(ripple);
        instance.ripples.Remove(ripple);
        Destroy(ripple.gameObject);
    }
}
