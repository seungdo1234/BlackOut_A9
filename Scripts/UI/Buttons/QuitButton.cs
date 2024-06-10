using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        // 구동 환경이 유니티 에디터일 경우
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // 구동 환경이 응용프로그램일 경우
#else
            Application.Quit();
#endif
    }
}
