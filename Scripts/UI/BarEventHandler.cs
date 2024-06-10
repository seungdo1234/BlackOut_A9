using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarEventHandler : MonoBehaviour
{
    private Image bar;

    private void Awake()
    {
        bar = GetComponent<Image>();
    }

    public void UpdateFillAmount(float maxValue, float curValue)
    {
        // bar의 fillamount를 수정
        bar.fillAmount = curValue / maxValue;
    }
}
