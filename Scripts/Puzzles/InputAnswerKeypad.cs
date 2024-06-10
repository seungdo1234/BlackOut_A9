using UnityEngine;

public class InputAnswerKeypad : MonoBehaviour
{
    public EPuzzleType type;

    public void CorrectAnswer()
    {
        switch (type)
        {
            case EPuzzleType.Dining:
                PuzzleManager.Instance.diningPuzzle.isClear = true;
                PuzzleManager.Instance.diningPuzzle.OpenPuzzleCase();
                break;
            case EPuzzleType.Floor2:
                PuzzleManager.Instance.secondFloorPuzzle.isClear = true;
                PuzzleManager.Instance.secondFloorPuzzle.OpenPuzzleCase();
                break;
            case EPuzzleType.Number:
                PuzzleManager.Instance.numberPuzzle.isClear = true;
                PuzzleManager.Instance.numberPuzzle.OpenPuzzleCase();
                break;
            case EPuzzleType.Final:
                PuzzleManager.Instance.finalPuzzle.ClearPuzzle();
                break;
        }
    }

    public void DeniedAnwer()
    {
        switch (type)
        {
            case EPuzzleType.Final:
                PuzzleManager.Instance.finalPuzzle.FailPuzzle();
                break;
        }
    }
}
