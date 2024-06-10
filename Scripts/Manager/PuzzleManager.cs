using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : Singleton<PuzzleManager>
{
    public DiningPuzzle diningPuzzle;
    public SecondFloorPuzzle secondFloorPuzzle;
    public NumberPuzzle numberPuzzle;
    public FinalPuzzle finalPuzzle;
    public CinemachineVirtualCamera noiseCam;
    public Image clearFadeOut;

    private void Awake()
    {
        finalPuzzle = gameObject.AddComponent<FinalPuzzle>();

        DontDestroyOnLoad(gameObject);
    }

    public void DestroyPuzzleManger()
    {
        if(gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
