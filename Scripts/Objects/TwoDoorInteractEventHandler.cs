using System;
using System.Collections;
using UnityEngine;

public class TwoDoorInteractEventHandler : MonoBehaviour
{
    [SerializeField] private float openSpeed = 200f;
    private bool isOpen = false;
    [SerializeField]private Transform leftDoorPivot;
    [SerializeField] private Transform rightDoorPivot;
    private Coroutine toggleCoroutine;

    public void TwoDoorToggleEvent()
    {
        isOpen = !isOpen;
        ToggleDoor();
    }

    private void ToggleDoor()
    {
        if (toggleCoroutine != null)
        {
            StopCoroutine(toggleCoroutine);
        }
        
        toggleCoroutine = StartCoroutine(TwoDoorToggleCoroutine());
    }

    private IEnumerator TwoDoorToggleCoroutine() // 문 두개 열고 닫히는 이벤트 코루틴
    {
        // 플레이어의 위치와 문의 위치를 비교하여 회전 각도 설정
        Vector3 dir = (GameManager.Instance.PlayerController.transform.position - transform.position).normalized;
    
        // 문이 닫히거나 열릴 때 양쪽문의 각도를 구해줌
        float leftTargetAngle = isOpen ? (dir.z > 0 ? -90 : 90) : 0;
        float rightTargetAngle = isOpen ? (dir.z > 0 ? 90 : -90) : 0;
        
        Quaternion leftTargetRotation = Quaternion.Euler(0, leftTargetAngle, 0);
        Quaternion rightTargetRotation = Quaternion.Euler(0, rightTargetAngle, 0);

        while (true)
        {
            // 현재 회전과 목표 회전 비교
            if (Quaternion.Angle(leftDoorPivot.localRotation, leftTargetRotation) < 0.1f && Quaternion.Angle(rightDoorPivot.localRotation, rightTargetRotation) < 0.1f)
            {
                leftDoorPivot.localRotation = leftTargetRotation;
                rightDoorPivot.localRotation = rightTargetRotation;
                break;
            }
            // 회전
            leftDoorPivot.localRotation = Quaternion.RotateTowards(leftDoorPivot.localRotation, leftTargetRotation, Time.deltaTime * openSpeed);
            rightDoorPivot.localRotation = Quaternion.RotateTowards(rightDoorPivot.localRotation, rightTargetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
    }
}