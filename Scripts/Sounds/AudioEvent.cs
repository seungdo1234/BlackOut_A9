using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioEvent : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayBgm(true);
    }
}
