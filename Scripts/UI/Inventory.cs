using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public Transform inventoryBg;

    public ItemSO selectedItem;
    public int selectedItemIndex;
    public Image itemImg;
    public TextMeshProUGUI descriptionText;


    private void Awake()
    {
        GameManager.Instance.Inventory = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        slots = new ItemSlot[inventoryBg.childCount];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = inventoryBg.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        GameManager.Instance.PlayerController.AddItem += AddItem;
        
    }


    public void SelectItem(int index)
    {
        if (slots[index].itemSO == null)
        {
            itemImg.enabled = false;
            itemImg.sprite = null;
            descriptionText.text = string.Empty;
            return;
        }

        selectedItem = slots[index].itemSO;
        selectedItemIndex = index;

        itemImg.enabled = true;
        itemImg.sprite = selectedItem.icon;
        descriptionText.text = selectedItem.description;

    }

    void AddItem(ItemSO data)
    {

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.itemSO = data;
            UpdateUI();
            return;
        }

    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemSO != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemSO == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    public bool IsItem(EItemType type)
    {
        foreach (var slot in slots)
        {
            if (slot.itemSO != null)
            {
                if (slot.itemSO.itemType == type)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public ItemSO FindItem(EItemType type)
    {
        foreach (var slot in slots)
        {
            if (slot.itemSO != null)
            {
                if (slot.itemSO.itemType == type)
                {
                    return slot.itemSO;
                }
            }
        }

        return null;
    }

    public void RemoveItem(EItemType type)
    {
        foreach (var slot in slots)
        {
            if (slot.itemSO != null)
            {
                if (slot.itemSO.itemType == type)
                {
                    slot.itemSO = null;
                    return;
                }
            }
        }

    }

}
