using UnityEngine;
using UnityEngine.SceneManagement;

public class BridgeChecker : MonoBehaviour
{
    public GameObject bridge;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        UpdateBridgeState();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateBridgeState();
    }

    void UpdateBridgeState()
    {
        if (GameManager.Instance == null || bridge == null) return;

        // ปิด bridge ถ้า puzzle ยังไม่เสร็จ
        bridge.SetActive(GameManager.Instance.puzzleCompleted);
    }
}
