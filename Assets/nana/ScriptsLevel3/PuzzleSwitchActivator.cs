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
                // รีเซ็ต flag ก่อนเข้า puzzle
                if (GameManager.Instance != null)
                    GameManager.Instance.puzzleCompleted = false;

                // บันทึกตำแหน่งผู้เล่น
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    GameManager.Instance.lastPlayerPosition = player.transform.position;
                    GameManager.Instance.hasSavedPosition = true;
                }

                // โหลด Puzzle Scene
                SceneManager.LoadScene("NewPuzzle");
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