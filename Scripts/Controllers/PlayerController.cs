using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> onMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnJumpEvent;
    public event Action OnInteractEvent;
    public event Action OnFlashEvent;
    public event Action OnFlashColorSwitchEvent;
    public event Action OnInventoryEvent;

    public Action<ItemSO> AddItem;
    public event Action<bool> OnZoomInEvent;


    protected void CallMoveEvent(Vector2 dir)
    {
        onMoveEvent?.Invoke(dir);
    }

    protected void CallLookEvent(Vector2 delta)
    {
        OnLookEvent?.Invoke(delta);
    }

    protected void CallJumpEvent()
    {
        OnJumpEvent?.Invoke();
    }

    protected void CallFlashEvent()
    {
        OnFlashEvent?.Invoke();
    }

    protected void CallInteractEvent()
    {
        OnInteractEvent?.Invoke();
    }

    protected void CallInventoryEvent()
    {
        OnInventoryEvent?.Invoke();
    }
    protected void CallFlashColorSwitchEvent()
    {
        OnFlashColorSwitchEvent?.Invoke();
    }
    protected void CallZoomInEvent(bool isPressed)
    {
        OnZoomInEvent?.Invoke(isPressed);
    }
}