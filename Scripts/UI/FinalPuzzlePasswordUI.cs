using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalPuzzlePasswordUI : MonoBehaviour
{
    public TextMeshProUGUI[] passwordText;

    private void Start()
    {
        foreach (var text in passwordText)
        {
            text.text = "?";
        }
    }

    public void UpdatePassword(int index, int password)
    {
        passwordText[index].text = password.ToString();
    }
}
