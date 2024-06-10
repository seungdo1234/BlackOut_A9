
using Cinemachine;
using UnityEngine;

public class CameraLookEventHandler : MonoBehaviour
{
    
    [Header("# Look")] 
    public float minXLook; // 마우스 X의 최대 최소 회전 값
    public float maxXLook;
    private float camCurXRot; // 마우스의 델타 값 저장
    public float lookSensitivity; // 회전 민감도
    private Vector2 mouseDelta;
    
    private Transform cameraContainer;
    private PlayerController controller;
    private CinemachineVirtualCamera playerCamera;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        cameraContainer = transform.GetChild(0);
        playerCamera = cameraContainer.GetComponentInChildren<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        // 마우스 커서를 안보이게 함
        Cursor.lockState = CursorLockMode.Locked;
        controller.OnLookEvent += SetMouseDelta;
        controller.OnZoomInEvent += ZoomIn;
    }

    private void SetMouseDelta(Vector2 delta)
    {
        mouseDelta = delta;
    }
    private void LateUpdate()
    {
        CameraLook();
    }
    private void CameraLook() // 위 아래는 카메라 x축 회전, 좌우는 플레이어 y축 회전
    {
        // 좌우로 돌릴 때 y축을 회전시켜줘야 좌우로 움직이게됨
        camCurXRot += mouseDelta.y * lookSensitivity;
        // 최대 최소 회전 값을 넘지 않게 함
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        // 카메라의 로컬 좌표를 돌려줌  => 위 아래 (x 회전이 -일 때 위를 바라봄)
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 플레어이어 y축 회전 -> 좌우 회전
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    private void ZoomIn(bool isPressed) // 카메라 줌인
    {
        if (isPressed)  // 줌인 키를 꾹 누르고 있을 때
        {
            playerCamera.m_Lens.FieldOfView = 20; // fov를 20으로
        }
        else // 줌인 키를 떼면
        {
            playerCamera.m_Lens.FieldOfView = 60; // fov를 60으로
        }
    }
}