using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PuzzleSwitchActivator : MonoBehaviour
{
    private bool playerInRange = false;
    public int requiredNumbers = 5;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory[] inventories = FindObjectsOfType<PlayerInventory>();
            int totalCollected = inventories.Sum(inv => inv.CollectedCount());

            if (totalCollected >= requiredNumbers)
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.puzzleCompleted = false;

                    // บันทึกชื่อฉากก่อนเข้า Puzzle
                    GameManager.Instance.previousScene = SceneManager.GetActiveScene().name;

                    // บันทึกตำแหน่งผู้เล่นไว้ก่อน (ใช้เมื่อตอบปริศนาเสร็จ)
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        GameManager.Instance.lastPlayerPosition = player.transform.position;
                        GameManager.Instance.hasSavedPosition = true;
                    }

                    // บอกว่าเราจะเข้า Puzzle
                    GameManager.Instance.isInPuzzle = true;
                    GameManager.Instance.returnFromPuzzle = false; // รีเซ็ต เผื่อค่านี้ยังถูกตั้งไว้ก่อนหน้า
                }

                SceneManager.LoadScene("NewPuzzle", LoadSceneMode.Single);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Shadow"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Shadow"))
            playerInRange = false;
    }
}