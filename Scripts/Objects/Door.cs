using UnityEngine;

public class Door : ItemObject
{
    [Header("# Door")]
    [SerializeField] protected float openSpeed = 200f;
    [SerializeField] protected bool isLock;
    protected bool isOpen = false;
    protected Coroutine toggleCoroutine;

    public bool IsLock
    {
        get => isLock;
        set => isLock = value;
    }

    protected bool UnLockDoor()
    {
        if (!isLock) {return true;}
        
        if (!GameManager.Instance.Inventory.IsItem(EItemType.KEY))
        {
            return false;
        }

        isLock = false;
        GameManager.Instance.Inventory.RemoveItem(EItemType.KEY);
        GameManager.Instance.Inventory.UpdateUI();
        return true;
    }
}