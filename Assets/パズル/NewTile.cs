using UnityEngine;
using UnityEngine.UI;

public class NewTile : MonoBehaviour
{
    [HideInInspector] public int originalIndex;   // index ที่ถูกต้องของ tile
    [HideInInspector] public int currentIndex;    // index ปัจจุบัน
    public Vector2Int pos;
    public bool isEmpty = false;

    private NewPuzzle15Manager manager;
    private Button button;
    private Image image;

    void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponentInChildren<Image>();
    }

    public void Init(NewPuzzle15Manager manager, Sprite sprite, int index, Vector2Int pos, bool makeEmpty = false)
    {
        this.manager = manager;
        this.originalIndex = index;
        this.currentIndex = index;
        this.pos = pos;

        if (makeEmpty) SetEmpty();
        else SetSprite(sprite);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    public bool IsAdjacent(Vector2Int otherPos)
    {
        return Mathf.Abs(pos.x - otherPos.x) + Mathf.Abs(pos.y - otherPos.y) == 1;
    }

    public void SetSprite(Sprite sprite)
    {
        isEmpty = false;
        if (image != null)
        {
            image.sprite = sprite;
            image.enabled = true;
        }
        button.interactable = true;
    }

    public void SetEmpty()
    {
        isEmpty = true;
        if (image != null)
        {
            image.sprite = null;
            image.enabled = false;
        }
        button.interactable = true;
    }

    public Sprite GetSprite()
    {
        return image != null ? image.sprite : null;
    }

    private void OnClick()
    {
        if (manager != null)
            manager.SwapTiles(this);
    }
}