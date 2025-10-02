using UnityEngine;

public class BridgeChecker : MonoBehaviour
{
    public GameObject bridge;

    void Start()
    {
        if (GameManager.Instance == null) return;

        bool completed = GameManager.Instance.puzzleCompleted;
        bridge.SetActive(completed);
    }
}