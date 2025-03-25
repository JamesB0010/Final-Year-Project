using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopItemChangeItemEffect : MonoBehaviour
{
    void Start()
    {
        Vector3 ogScale = transform.localScale;
        transform.localScale.LerpTo(transform.localScale * 1.2f, 0.1f, val => transform.localScale = val, pkg =>
        {
            transform.localScale.LerpTo(ogScale, 0.05f, val => transform.localScale = val);
        });
    }
}
