using UnityEngine;

public class ItemObject : MonoBehaviour , IInteractable
{
    public ItemSO data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public virtual void OnInteract() // TODO: 상호작용 아직 개발해야 함
    {
        GameManager.Instance.PlayerController.AddItem?.Invoke(data);
        Destroy(gameObject);
        GameManager.Instance.PlayerController.OnInteractEvent -= this.OnInteract; // 상호작용되면 상호작용 해제
        if(data.itemType != EItemType.DOOR)
        {
            AudioManager.Instance.PlaySfx(EItemType.GETITEM);
        }
    }

    public ItemSO GetItemData()
    {
        return data;
    }
}