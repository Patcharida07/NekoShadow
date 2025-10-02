using UnityEngine;

public class PuzzleCompleteUI : MonoBehaviour
{
    public GameObject completeImage;

    public void ShowCompleteImage()
    {
        if (completeImage != null)
            completeImage.SetActive(true);
    }
}