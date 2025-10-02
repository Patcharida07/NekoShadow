using UnityEngine;

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
        }
    }
}