using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RippleManager : MonoBehaviour
{
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

    public static void RegisterNewRipple(Ripple ripple) => instance.ripples.Add(ripple);
    
    private void OnDestroy()
    {
        //Clean up
        instance = null;
        Destroy(this.gameObject);
    }

    public static void DestroyRipple(Ripple bestRipple)
    {
        instance.ripples.Remove(bestRipple);
        Destroy(bestRipple.gameObject);
    }
}
