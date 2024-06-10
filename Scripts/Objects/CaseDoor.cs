using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseDoor : Door
{
    [SerializeField] protected bool isLeft;

    public override void OnInteract()
    {

        if (isLock) return;

        isOpen = !isOpen;
        ToggleDoor();

        AudioManager.Instance.PlaySfx(EItemType.CASEDOOR);
    }

    protected void ToggleDoor()
    {
        if (toggleCoroutine != null)
        {
            StopCoroutine(toggleCoroutine);
        }

        toggleCoroutine = StartCoroutine(ToggleDoorCoroutine());
    }

    private IEnumerator ToggleDoorCoroutine()
    {
        // 목표 회전 각도 설정
        float targetAngle = isOpen ? (isLeft ? 90f : -90f) : 0f;
        float xRot = isLeft ? -180f : 0f;
        Quaternion targetRotation = Quaternion.Euler(xRot, targetAngle, 0);

        while (true)
        {
            // 현재 회전과 목표 회전 비교
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                transform.localRotation = targetRotation;
                break;
            }
            // 회전
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
    }

}

