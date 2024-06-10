using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorPuzzle : Puzzle
{
    public CaseDoor[] caseDoors;

    private void Awake()
    {
        PuzzleManager.Instance.secondFloorPuzzle = this;

        LockPuzzleCase();
    }

    public void LockPuzzleCase()
    {
        foreach (var caseDoor in caseDoors)
        {
            caseDoor.IsLock = true;
        }
    }

    public void OpenPuzzleCase()
    {
        foreach (var caseDoor in caseDoors)
        {
            caseDoor.IsLock = false;
        }
    }

}
