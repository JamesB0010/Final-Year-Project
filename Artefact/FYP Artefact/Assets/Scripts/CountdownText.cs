using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CountdownText : MonoBehaviour
{
    private TextMeshProUGUI TMPROText { get; set; }

    private FloatLerpPackage countdownPackage;

    private void Awake()
    {
        this.TMPROText = GetComponent<TextMeshProUGUI>();
        this.TMPROText.enabled = false;
    }
    public void StartCountdown(float countFrom)
    {
        countFrom += 1;
        this.TMPROText.enabled = true;

        this.countdownPackage = countFrom.LerpTo(0f, countFrom, val => this.TMPROText.text = ((int)val).ToString());
    }

    public void StopCountdownAndHide()
    {
        GlobalLerpProcessor.RemovePackage(this.countdownPackage);
        this.TMPROText.enabled = false;
    }
}
