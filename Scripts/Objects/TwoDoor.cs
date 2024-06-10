using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDoor : Door
{
    [SerializeField] private TwoDoor otherDoor;
    private TwoDoorInteractEventHandler twoDoorInteractEventHandler;
    private void Awake()
    {
        Transform pivot = transform.parent;
        twoDoorInteractEventHandler = pivot.parent.gameObject.GetComponent<TwoDoorInteractEventHandler>();
    }

    public override void OnInteract()
    {
        if(!UnLockDoor())return;
        else
        {
            if (otherDoor.isLock)
            {
                otherDoor.isLock = false;
            }
        }
        twoDoorInteractEventHandler.TwoDoorToggleEvent();
        AudioManager.Instance.PlaySfx(EItemType.DOOR);
    }
}