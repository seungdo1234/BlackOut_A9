using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealPuzzle : MonoBehaviour
{
    public Light spotLight;
    public GameObject revealObj;
    private Coroutine coroutine;

    public IEnumerator CheckSpotLightColor()
    {
        while (true)
        {
            // 손전등이 활성화되어있고, 색깔이 파란색인경우 revealObj 활성화
            if (spotLight.color == Color.blue && spotLight.isActiveAndEnabled)
            {
                revealObj.SetActive(true);
            }
            else
            {
                revealObj.SetActive(false);
            }
            yield return null;     
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        // 식당에 진입하면 손전등 색상확인 코루틴 시작 
        if (other.CompareTag("Player"))
        {
            coroutine = StartCoroutine(CheckSpotLightColor());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(coroutine);
        coroutine = null;
        revealObj.SetActive(false);
    }


}
