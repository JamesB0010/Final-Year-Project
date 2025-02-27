using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility;
using Random = UnityEngine.Random;

public class ArmRaiseBox : MonoBehaviour
{
    [SerializeField] private float minTimeRequiredToFill;

    [SerializeField] private float maxTimeRequiredToFill;
    
    [SerializeField] private Image image;

    [SerializeField] private UnityEvent Full;
    
    private float timeRequiredToFill;
    private float fillAmount;
    private bool filling;
    private bool full;

    public void RaiseArmSectionStarted()
    {
        this.timeRequiredToFill = Random.Range(this.minTimeRequiredToFill, this.maxTimeRequiredToFill);
        this.fillAmount = 0;
        this.image.color = Color.red;
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
        if(!filling || this.full)
            return;

        this.fillAmount += Time.deltaTime;
        
        if (this.fillAmount >= timeRequiredToFill)
        {
            this.full = true;
            this.Full?.Invoke();
            this.filling = false;
        }

        this.image.color = Color.Lerp(Color.red, Color.green, Mathf.Clamp01(ValueInRangeMapper.MapRange(this.fillAmount, 0, this.timeRequiredToFill, 0, 1)));
    }

    public void ActivateBox()
    {
        this.image.gameObject.SetActive(true);
        this.enabled = true;
    }
}
