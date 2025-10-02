using UnityEngine;

public class ResetOnFall : MonoBehaviour
{
    public Transform player;
    public Transform shadow;

    public Transform playerStartPoint;
    public Transform shadowStartPoint;

    public PlatformFadeOut eraser;
    public StartEraseTrigger eraseTrigger;

    public Vector3 respawnOffset = new Vector3(0, 1f, 0);

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"⚡ ResetOnFall triggered by {other.name} (tag: {other.tag})");

        if (other.CompareTag("Player"))
        {
            Respawn(player, playerStartPoint);
        }
        else if (other.CompareTag("Shadow"))
        {
            Respawn(shadow, shadowStartPoint);
        }
    }

    private void Respawn(Transform target, Transform startPoint)
    {
        Vector3 newPos = startPoint.position + respawnOffset;
        target.position = newPos;

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        Debug.Log($"📍 {target.name} respawned at {newPos}");

        eraser.ResetPlatforms();
        eraseTrigger.ResetTrigger();
    }
}