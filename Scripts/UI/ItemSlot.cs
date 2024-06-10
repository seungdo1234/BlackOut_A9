using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemSO itemSO;
    public Image icon;
    public int index;

    public Inventory inventory;

    public void Set()
    {
        icon.enabled = true;
        icon.gameObject.SetActive(true);
        icon.sprite = itemSO.icon;
    }

    public void Clear()
    {
        itemSO = null;
        icon.enabled = false;
        icon.gameObject.SetActive(false);
    }

    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }

}