using UnityEngine;

public class BasementPaper : ItemObject
{
    [SerializeField] private EItemType targetItemType;
    [SerializeField] private CaseDoor caseDoor;
    [SerializeField] private GameObject flower;
    public override void OnInteract()
    {
        if (!GameManager.Instance.Inventory.IsItem(targetItemType))
        {
            return;
        }
        
        GameManager.Instance.Inventory.RemoveItem(targetItemType);
        GameManager.Instance.Inventory.UpdateUI();
        
        caseDoor.IsLock = false;
        caseDoor.OnInteract();
        flower.SetActive(true);
        gameObject.SetActive(false);
    }
}