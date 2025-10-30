using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameUIManager : MonoBehaviour
{
    public GameObject menuPanel;        // Menu panel
    public GameObject howToPlayPanel;   // How To Play panel

    // ปุ่ม Menu → เปิดเมนู
    public void OnMenuButton()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);  // แสดงเมนู
            Time.timeScale = 0f;        // หยุดเกมชั่วคราว
        }
    }

    // ปุ่มปิดเมนู → Resume เกม
    public void OnCloseMenu()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(false); // ซ่อนเมนู
            Time.timeScale = 1f;        // เล่นเกมต่อ
        }
    }

    public void OnHomeButton()
    {
        Time.timeScale = 1f;            // เล่นเกมต่อก่อนโหลด Scene
        SceneManager.LoadScene("Start"); // ชื่อ Scene เมนูหลัก
    }


    // ปุ่ม Restart → รีสตาร์ทเกม
    public void OnRestart()
    {
        Time.timeScale = 1f;

        // รีเซ็ต GameManager เพื่อให้ bridge ปิด
        if (GameManager.Instance != null)
        {
            GameManager.Instance.puzzleCompleted = false;
            GameManager.Instance.returnFromPuzzle = false;
            GameManager.Instance.hasSavedPosition = false;
            GameManager.Instance.isInPuzzle = false;
        }

        // โหลด scene ปัจจุบันใหม่ → รีเซ็ตทุกอย่าง
        string current = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(current, LoadSceneMode.Single);
    }

    // ปุ่ม How To Play → เปิด Panel วิธีเล่น
    public void OnHowToPlay()
    {
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(true);
            Time.timeScale = 0f;        // หยุดเกมชั่วคราว
        }
    }

    // ปิด Panel วิธีเล่น → กลับมาเล่นเกม
    public void OnCloseHowToPlay()
    {
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}