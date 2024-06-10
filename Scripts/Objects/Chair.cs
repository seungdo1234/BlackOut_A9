using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chair : ItemObject
{
    public GameObject chairViewUI; // UI
    public Camera cameraObject; // 카메라
    public PlayerInput playerInput; // 입력

    public override void OnInteract()
    {
        OpenCameraUI();
    }

    private void OpenCameraUI()
    {
        Cursor.lockState = CursorLockMode.None;
        chairViewUI.SetActive(true);
        cameraObject.gameObject.SetActive(true);
        playerInput.enabled = false;
    }

    public void CloseCameraUI()
    {
        Cursor.lockState = CursorLockMode.Locked;
        chairViewUI.SetActive(false);
        cameraObject.gameObject.SetActive(false);
        playerInput.enabled = true;
    }
}
