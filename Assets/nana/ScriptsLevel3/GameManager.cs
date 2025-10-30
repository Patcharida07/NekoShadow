using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool puzzleCompleted = false;      // true เมื่อ Puzzle เสร็จ
    public bool hasSavedPosition = false;     // true ถ้า player มีตำแหน่งบันทึก
    public bool returnFromPuzzle = false;     // true เมื่อกลับจาก Puzzle หลังเสร็จ
    public bool isInPuzzle = false;           // true ขณะอยู่ใน Puzzle
    public Vector3 lastPlayerPosition;        // บันทึกตำแหน่ง player
    public string previousScene;              // บันทึกชื่อ scene ก่อนเข้า Puzzle

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ถ้าโหลด Level3 หรือ Scene ก่อนหน้าและ returnFromPuzzle == true
        if (scene.name == previousScene && hasSavedPosition && returnFromPuzzle)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = lastPlayerPosition; // ย้าย player
            }
            returnFromPuzzle = false;
            hasSavedPosition = false;
            previousScene = null;
        }

        // ถ้าโหลด Puzzle
        if (scene.name == "NewPuzzle")
        {
            isInPuzzle = true;
            returnFromPuzzle = false;
        }
        else
        {
            isInPuzzle = false;
        }
    }
}