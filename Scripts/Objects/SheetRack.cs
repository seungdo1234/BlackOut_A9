using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetRack : Door
{
    [Header("# SheetRack")]
    [SerializeField] private float targetPosZ = -0.4f; 
    private Vector3 initPos; // 초기 위치
    private Vector3 targetPos; // 서랍을 여는 목표 위치

    private void Awake()
    {
        initPos = transform.localPosition;
        targetPos = new Vector3(initPos.x, initPos.y, targetPosZ);
    }

    public override void OnInteract()
    {
        if(isLock) return;
        
        isOpen = !isOpen;
        ToggleDoor();
        AudioManager.Instance.PlaySfx(EItemType.CASEDOOR);
    }

    private void ToggleDoor()
    {
        if (toggleCoroutine != null)
        {
            StopCoroutine(toggleCoroutine);
        }
        
        toggleCoroutine = StartCoroutine(ToggleSheetRackCoroutine(isOpen ? targetPos : initPos));
    }

    private IEnumerator ToggleSheetRackCoroutine(Vector3 targetPosition) // 서랍 열고 닫기
    {
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, openSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = targetPosition;
    }
}
