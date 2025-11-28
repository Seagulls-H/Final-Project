using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.OnPlayerKilled();
            return;
        }

        var death = other.GetComponent<PlayerDeath>();
        if (death != null)
        {
            death.Die();
        }
        else
        {
            GameManager.RespawnPlayer();
        }
    }
}
