using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopItemChangeItemEffect : MonoBehaviour
{
    private Vector3LerpPackage sizeLerpPackage;
    void Start()
    {
        Vector3 ogScale = transform.localScale;
        this.sizeLerpPackage = transform.localScale.LerpTo(transform.localScale * 1.2f, 0.1f, val => transform.localScale = val, pkg =>
        {
            if (transform == null)
                return;
            transform.localScale.LerpTo(ogScale, 0.05f, val => transform.localScale = val);
        });
    }

    private void OnDestroy()
    {
        if(sizeLerpPackage != null)
            GlobalLerpProcessor.RemovePackage(this.sizeLerpPackage);
    }
}
