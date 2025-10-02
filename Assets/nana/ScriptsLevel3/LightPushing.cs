using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class LightPushing : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.mass = 500f;
        rb.linearDamping = 2f;
        rb.angularDamping = 15f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void FixedUpdate()
    {
        if (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // Player ดันได้
        }
        else if (collision.collider.CompareTag("Shadow"))
        {
            rb.bodyType = RigidbodyType2D.Kinematic; // Shadow ไม่ดัน (คงที่ แต่ยังชนได้)
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Shadow"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // กลับมา Dynamic ปกติ
        }
    }
}
