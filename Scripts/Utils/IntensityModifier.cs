using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntensityModifier : MonoBehaviour
{

    private List<Light> lights;
   
    [ContextMenu("IntensityModify")]
    private void Modify()
    {
        // 하이어라키에 있는 모든 라이트 오브젝트를 찾음
        lights = new List<Light>(FindObjectsOfType<Light>());

        // 각 라이트의 Intensity를 30% 줄임
        foreach (var light in lights)
        {
            light.intensity *= 1.3f;
        }
    }
}
