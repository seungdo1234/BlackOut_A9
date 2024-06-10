using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void SceneChange(int sceneNum)
    {
        PuzzleManager.Instance.DestroyPuzzleManger();
        SceneManager.LoadScene(sceneNum);
        SceneManager.sceneLoaded += CompleteChangeScene;
    }

    void CompleteChangeScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.isLoaded)
        {
            AudioManager.Instance.PlayBgm(false);
        }
    }
}
