using UnityEngine;

public class PuzzleResetter : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.puzzleCompleted = false;
        }
    }
}