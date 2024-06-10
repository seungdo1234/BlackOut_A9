using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    private bool isControlLock;
    private void Awake()
    {
        GameManager.Instance.PlayerController = this;
        OnInventoryEvent += ControlLocked;
    }

    public void ControlLocked()
    {
        isControlLock = !isControlLock;

        Cursor.lockState = isControlLock ? CursorLockMode.None : CursorLockMode.Locked;
        
        if (isControlLock)
        {
            CallLookEvent(Vector2.zero);
            CallMoveEvent(Vector2.zero);
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isControlLock && context.phase == InputActionPhase.Performed) // 키가 계속 눌리는 중
        {
            CallMoveEvent(context.ReadValue<Vector2>());
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            CallMoveEvent(Vector2.zero);
        }
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        if (!isControlLock)
        {
            CallLookEvent(context.ReadValue<Vector2>());   
        }
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!isControlLock && context.phase == InputActionPhase.Started)
        {
            CallJumpEvent();
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!isControlLock && context.phase == InputActionPhase.Started)
        {
            CallInteractEvent();
        }
    }
    public void OnFlash(InputAction.CallbackContext context)
    {
        if (!isControlLock && context.phase == InputActionPhase.Started)
        {
            CallFlashEvent();
        }
    }
    
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            CallInventoryEvent();
        }
    }
    
    public void OnFlashColorSwitch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            CallFlashColorSwitchEvent();
        }
    }

    public void OnZoomIn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            CallZoomInEvent(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            CallZoomInEvent(false);
        }
    }
}