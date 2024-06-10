using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDoor : Door
{
    private Quaternion targetRotation;
    private bool isRot // 문이 회전 되어 있는 지
    {
        get => transform.parent.rotation.y % 180 != 0;
    }
    
    public override void OnInteract()
    {
        if(!UnLockDoor()) return;
        
        isOpen = !isOpen;
        AudioManager.Instance.PlaySfx(EItemType.DOOR);
        ToggleDoor();
    }

    private void ToggleDoor()
    {
        if (toggleCoroutine != null)
        {
            StopCoroutine(toggleCoroutine);
        }

        // 문이 열리는 각도 구하기
        Vector3 dir = (GameManager.Instance.PlayerController.transform.position - transform.position).normalized;
        float targetAngle = isOpen ? GetOpenTargetAngle(dir) : 0;
        targetRotation = Quaternion.Euler(0, targetAngle, 0);
        
        toggleCoroutine = StartCoroutine(DoorToggleCoroutine());
    }
    
    private IEnumerator DoorToggleCoroutine()
    {
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
    
    private float GetOpenTargetAngle(Vector3 dir)
    {
        return isRot ? (dir.x > 0 ? 90 : -90) : (dir.z > 0 ? -90 : 90);
    }
    
}
