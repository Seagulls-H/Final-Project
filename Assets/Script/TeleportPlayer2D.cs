using UnityEngine;

public class TeleportPlayer2D : MonoBehaviour
{
    public Transform targetPoint;          
    public bool resetVelocity = true;      

    [Header("Required direction (optional)")]
    public bool useDirectionCheck = false; 
    public bool requireMoveRight = true;   

    [Header("Anti-continuous teleport")]
    public float globalCooldown = 0.2f;    
    private static float lastTeleportTime = -999f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (Time.time - lastTeleportTime < globalCooldown)
            return;

        Rigidbody2D rb = other.attachedRigidbody;

        if (useDirectionCheck && rb != null)
        {
            bool movingRight = rb.linearVelocity.x > 0.01f;

            if (requireMoveRight && !movingRight)
                return; 

            if (!requireMoveRight && movingRight)
                return; 
        }

        if (resetVelocity && rb != null)
            rb.linearVelocity = Vector2.zero;

        if (targetPoint != null)
        {
            other.transform.position = targetPoint.position;
            lastTeleportTime = Time.time;
        }
        else
        {
            Debug.LogWarning("TeleportPlayer2D: No targetPoint assigned!");
        }
    }
}
