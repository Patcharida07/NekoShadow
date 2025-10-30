using UnityEngine;

public class PlayerPositionRestorer : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.hasSavedPosition)
        {
            transform.position = GameManager.Instance.lastPlayerPosition;
            GameManager.Instance.hasSavedPosition = false; // ใช้แล้วเคลียร์ค่า
        }
    }
}
