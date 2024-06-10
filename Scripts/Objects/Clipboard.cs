using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clipboard : MonoBehaviour
{
    private ItemObject itemObject;
    private TextMeshPro textUI;

    private void Awake()
    {
        itemObject = GetComponent<ItemObject>();
        textUI = GetComponentInChildren<TextMeshPro>();
        SetText(itemObject.data.text);
    }

    private void SetText(string text)
    {
        textUI.text = text;
    }
}
