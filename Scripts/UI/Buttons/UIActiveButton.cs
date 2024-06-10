using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActiveButton : MonoBehaviour
{
    public GameObject ui;

    public void OpenUI()
    {
        ui.gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        ui.gameObject.SetActive(false);
    }
}
