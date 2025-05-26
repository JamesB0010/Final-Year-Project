using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetGestureImages : MonoBehaviour
{
    [SerializeField] private Image[] gestureImages;
    
    public void ResetImageColors()
    {
        foreach (Image gestureImage in this.gestureImages)
        {
            gestureImage.color = Color.red;
        }
    }
}
