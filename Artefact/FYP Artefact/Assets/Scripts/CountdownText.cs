using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownText : MonoBehaviour
{
    private TextMeshProUGUI TMPROText { get; set; }

    private FloatLerpPackage countdownPackage;

    private void Awake()
    {
        this.TMPROText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        this.TMPROText.enabled = false;
    }

    public void StartCountdown(float countFrom)
    {
        this.TMPROText.enabled = true;
        this.TMPROText.text = countFrom.ToString();

        countFrom += 1;

        this.countdownPackage = countFrom.LerpTo(0f, countFrom, val => this.TMPROText.text = ((int)val).ToString());
    }

    public void LostLockOn()
    {
        GlobalLerpProcessor.RemovePackage(this.countdownPackage);
        this.TMPROText.enabled = false;
    }

    public void LockedOntoFish()
    {
        this.TMPROText.enabled = false;
    }
}
