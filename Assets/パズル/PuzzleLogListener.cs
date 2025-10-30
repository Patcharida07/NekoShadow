using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PuzzleLogListener : MonoBehaviour
{
    public GameObject completeImage;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (logString.Contains("🎉 Puzzle Complete!"))
        {
            if (completeImage != null)
                completeImage.SetActive(true);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.puzzleCompleted = true;   // เปิด bridge
                GameManager.Instance.returnFromPuzzle = true;  // flag กลับตำแหน่ง player
                GameManager.Instance.isInPuzzle = false;
            }

            StartCoroutine(ReturnToPrevious());
        }
    }

    IEnumerator ReturnToPrevious()
    {
        yield return new WaitForSeconds(2f); // ให้ผู้เล่นเห็น completeImage
        if (GameManager.Instance != null && !string.IsNullOrEmpty(GameManager.Instance.previousScene))
            SceneManager.LoadScene(GameManager.Instance.previousScene, LoadSceneMode.Single);
    }
}