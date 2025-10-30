using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void againButton()
    {
        Debug.Log("again");

        // รีเซ็ต GameManager เพื่อให้ bridge / puzzle รีเซ็ต
        if (GameManager.Instance != null)
        {
            GameManager.Instance.puzzleCompleted = false;
            GameManager.Instance.returnFromPuzzle = false;
            GameManager.Instance.hasSavedPosition = false;
            GameManager.Instance.isInPuzzle = false;
        }

        // ถ้า pause อยู่ ให้กลับมาเล่นก่อน
        Time.timeScale = 1f;

        // โหลด Level3 ตามชื่อใน Build Settings
        SceneManager.LoadScene("Level3", LoadSceneMode.Single);
    }

    public void returnButton()
    {
        Debug.Log("return");
        // กลับเมนูหลัก
        SceneManager.LoadScene("Start");
    }
}