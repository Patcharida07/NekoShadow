using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPuzzle15Manager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject tilePrefab;
    public Transform gridParent;
    public List<Sprite> sprites; // 8 หรือ 15 รูปตาม puzzle size

    private List<NewTile> tiles = new List<NewTile>();
    private const int size = 3; // 3x3
    private bool isShuffling = false;

    void Start()
    {
        GenerateTiles();

        // สุ่มจนกว่าจะไม่ complete
        do { ShuffleTiles(); }
        while (IsComplete());
    }

    void GenerateTiles()
    {
        tiles.Clear();

        for (int i = 0; i < size * size; i++)
        {
            GameObject tileObj = Instantiate(tilePrefab, gridParent);
            NewTile tile = tileObj.GetComponent<NewTile>();
            Vector2Int pos = new Vector2Int(i % size, i / size);

            if (i == size * size - 1) // ช่องสุดท้ายเป็น empty
                tile.Init(this, null, i, pos, true);
            else
                tile.Init(this, sprites[i], i, pos, false);

            tiles.Add(tile);
        }
    }

    void ShuffleTiles()
    {
        isShuffling = true;

        for (int i = 0; i < 100; i++)
        {
            NewTile emptyTile = GetEmptyTile();
            if (emptyTile == null) continue;

            List<NewTile> neighbors = GetAdjacentTiles(emptyTile);
            if (neighbors.Count == 0) continue;

            NewTile swapTile = neighbors[Random.Range(0, neighbors.Count)];
            SwapTiles(swapTile, false); // shuffle ไม่เช็ก complete
        }

        isShuffling = false;
    }

    public NewTile GetEmptyTile()
    {
        return tiles.Find(t => t.isEmpty);
    }

    // Swap แบบมี option checkComplete
    public void SwapTiles(NewTile clickedTile, bool checkComplete)
    {
        var emptyTile = GetEmptyTile();
        if (emptyTile == null) return;
        if (clickedTile == null) return;
        if (clickedTile.isEmpty) return;
        if (!clickedTile.IsAdjacent(emptyTile.pos)) return;

        Sprite movingSprite = clickedTile.GetSprite();
        int movingIndex = clickedTile.currentIndex;

        emptyTile.SetSprite(movingSprite);
        emptyTile.isEmpty = false;
        emptyTile.currentIndex = movingIndex;

        clickedTile.SetSprite(null);
        clickedTile.isEmpty = true;
        clickedTile.currentIndex = -1;

        if (checkComplete && !isShuffling) CheckComplete();
    }

    // Overload สำหรับเรียกปกติ
    public void SwapTiles(NewTile clickedTile)
    {
        SwapTiles(clickedTile, true);
    }

    List<NewTile> GetAdjacentTiles(NewTile tile)
    {
        List<NewTile> adj = new List<NewTile>();
        foreach (var t in tiles)
        {
            if (t == tile) continue;
            if (Mathf.Abs(t.pos.x - tile.pos.x) + Mathf.Abs(t.pos.y - tile.pos.y) == 1)
                adj.Add(t);
        }
        return adj;
    }

    bool IsComplete()
    {
        foreach (var tile in tiles)
        {
            if (!tile.isEmpty && tile.currentIndex != tile.originalIndex)
                return false;
        }
        return true;
    }

    void CheckComplete()
    {
        if (IsComplete())
        {
            Debug.Log("🎉 Puzzle Complete!");

            // บันทึกสถานะให้ GameManager รู้ว่าพาสเซิลผ่านแล้ว (ถ้ามีใช้)
            if (GameManager.Instance != null)
            {
                GameManager.Instance.puzzleCompleted = true;
                Debug.Log("GameManager puzzleCompleted = true");
            }

            // โหลด Scene3 (ใส่ชื่อจริงของ scene ตามที่ตั้งไว้ใน Build Settings)
            SceneManager.LoadScene("Level3");
        }
    }
}
