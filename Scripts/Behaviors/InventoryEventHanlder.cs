using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryEventHanlder : MonoBehaviour
{
    private bool isInventoryOn;
    private Canvas canvas;
    private Inventory inventory;
    
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = true;
        inventory = GetComponent<Inventory>();
    }

    private void Start()
    {
        GameManager.Instance.PlayerController.OnInventoryEvent += ToggleInventory;
        gameObject.SetActive(false);
    }

    private void ToggleInventory()
    {
        isInventoryOn = !isInventoryOn;
        gameObject.SetActive(isInventoryOn);

        if (isInventoryOn)
        {
            inventory.descriptionText.text = "";
            inventory.itemImg.enabled = false;
        }
    }
}
