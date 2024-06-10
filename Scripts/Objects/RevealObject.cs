using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealObject : MonoBehaviour
{
    [SerializeField] Material Mat;
    [SerializeField] Light SpotLight;

    void Update()
    {
        Mat.SetVector("MyLightPosition", SpotLight.transform.position);
        Mat.SetVector("MyLightDirection", -SpotLight.transform.forward);
        Mat.SetFloat("MyLightAngle", SpotLight.spotAngle);
    }
}
