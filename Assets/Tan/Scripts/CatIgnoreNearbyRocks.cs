using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CatIgnoreNearbyRocks : MonoBehaviour
{
    [Header("設定")]
    public string rockTag = "gravel";          // 石頭物件請設定 Tag = "Rock"
    public float ignoreRadius = 1.0f;       // 靠近多少範圍內就忽略碰撞

    [Header("Layer (選用，能加速檢測)")]
    public LayerMask rockLayer;             // 如果你把石頭放在特定 Layer，請設定

    Collider2D catCollider;
    // 被忽略碰撞的石頭集合（快查）
    private HashSet<Collider2D> ignoredRocks = new HashSet<Collider2D>();

    void Awake()
    {
        catCollider = GetComponent<Collider2D>();
        if (catCollider == null) Debug.LogError("需要 Collider2D 在同一物件上。");
    }

    void FixedUpdate()
    {
        // 用 OverlapCircleAll 找附近的 collider
        Collider2D[] hits;
        if (rockLayer.value != 0)
            hits = Physics2D.OverlapCircleAll(transform.position, ignoreRadius, rockLayer);
        else
            hits = Physics2D.OverlapCircleAll(transform.position, ignoreRadius);

        HashSet<Collider2D> found = new HashSet<Collider2D>();

        foreach (var c in hits)
        {
            if (c == null) continue;
            if (!c.CompareTag(rockTag)) continue; // 確認 tag
            if (c == catCollider) continue;

            found.Add(c);

            if (!ignoredRocks.Contains(c))
            {
                // 新加入：忽略碰撞
                Physics2D.IgnoreCollision(catCollider, c, true);
                ignoredRocks.Add(c);
            }
        }

        // 檢查哪些之前被忽略但現在已不在範圍內 -> 恢復碰撞
        var toRemove = new List<Collider2D>();
        foreach (var ic in ignoredRocks)
        {
            if (!found.Contains(ic))
            {
                Physics2D.IgnoreCollision(catCollider, ic, false);
                toRemove.Add(ic);
            }
        }

        foreach (var r in toRemove) ignoredRocks.Remove(r);
    }

    void OnDisable()
    {
        // 遊戲物件停用時恢復所有忽略的碰撞，避免遺留狀態
        foreach (var c in ignoredRocks)
            if (c != null)
                Physics2D.IgnoreCollision(catCollider, c, false);
        ignoredRocks.Clear();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ignoreRadius);
    }
}

