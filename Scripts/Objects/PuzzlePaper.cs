using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzlePaper : ItemObject
{
    private TextMeshPro text;
    public int puzzleNum;
    private FinalPuzzle finalPuzzle;

    private void Awake()
    {
        finalPuzzle = PuzzleManager.Instance.finalPuzzle;
    }

    private void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
        text.text = finalPuzzle.password[puzzleNum].ToString();
    }

    public override void OnInteract()
    {
        finalPuzzle.FindPassword(puzzleNum);
        Destroy(gameObject);
    }
}
