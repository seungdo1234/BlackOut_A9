using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearEventHandler : MonoBehaviour
{
    [Header("# Fear")]
    [SerializeField] private float maxFearValue;
    [SerializeField] private float fearIncreaseRateWhenLightOn; // 라이트를 켰을 때 두려움이 올라가는 속도
    [SerializeField] private float fearIncreaseRateWhenLightOff; // 라이트를 껐을 때 두려움이 올라가는 속도
    [SerializeField] private float curFearValue;

    [Header("# Fear Bar")]
    [SerializeField] private BarEventHandler barEventHandler;
    
    [Header("# FlashLight Info")]
    [SerializeField] private FlashLightEventHandler flash;

    private Coroutine fearIncreaseCoroutine;
    private bool soundCheck;
    private AudioManager audioManager;

    private void Start()
    {
        IncreaseFear();
        audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        FearBarCheck();
    }

    private void IncreaseFear()
    {
        if (fearIncreaseCoroutine != null)
        {
            StopCoroutine(fearIncreaseCoroutine);
        }

        fearIncreaseCoroutine = StartCoroutine(FearIncreaseCoroutine());
    }
    private IEnumerator FearIncreaseCoroutine()
    {
        while (true)
        {
            if (curFearValue >= maxFearValue)
            {
                GameManager.Instance.GameOver();
                break;
            }
            
            curFearValue += flash.IsFlashlightOn ? 
                Time.deltaTime * fearIncreaseRateWhenLightOn : Time.deltaTime * fearIncreaseRateWhenLightOff; 
            barEventHandler.UpdateFillAmount(maxFearValue,curFearValue);
            yield return null;
        }
    }

    public void AddFear(float amount)
    {
        curFearValue += amount;
        barEventHandler.UpdateFillAmount(maxFearValue, curFearValue);
    }
    
    private void FearBarCheck()
    {   
        bool isHeartBeatPlaying = audioManager.IsHeartbeatPlaying();

        if (curFearValue >= maxFearValue)
        { 
            if(isHeartBeatPlaying)
            {
                audioManager.HeartBeatSfx(false);
            }
        }
        else if(curFearValue >= maxFearValue * 0.8f)
        {
            if (!isHeartBeatPlaying)
            {
                audioManager.HeartBeatSfx(true);
            }
        }
    }
}
