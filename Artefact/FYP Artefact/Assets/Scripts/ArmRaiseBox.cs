using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility;
using Random = UnityEngine.Random;

public class ArmRaiseBox : MonoBehaviour
{
    [SerializeField] private float minTimeRequiredToFill;

    [SerializeField] private float maxTimeRequiredToFill;

    private float timeRequiredToFill;

    private float fillAmount;

    private bool filling;

    [SerializeField] private UnityEvent Full;
    

    private Image image;

    private void Awake()
    {
        this.image = GetComponent<Image>();
    }

    public void RaiseArmSectionStarted()
    {
        this.timeRequiredToFill = Random.Range(this.minTimeRequiredToFill, this.maxTimeRequiredToFill);
        this.fillAmount = 0;
    }

    public void StartFilling()
    {
        this.filling = true;
    }

    public void StopFilling()
    {
        this.filling = false;
    }

    private void Update()
    {
        if(!filling)
            return;

        this.fillAmount += Time.deltaTime;
        
        if (this.fillAmount >= timeRequiredToFill)
        {
            this.Full?.Invoke();
            this.filling = false;
        }

        this.image.color = Color.Lerp(Color.red, Color.green, Mathf.Clamp01(ValueInRangeMapper.MapRange(this.fillAmount, 0, this.timeRequiredToFill, 0, 1)));
    }
}
