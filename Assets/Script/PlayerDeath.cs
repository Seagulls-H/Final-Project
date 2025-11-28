using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDeath : MonoBehaviour
{
    public float respawnDelay = 1.2f;

    private Animator anim;
    private PlayerController controller;
    private Rigidbody2D rb;
    private bool isDead = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (controller != null) controller.enabled = false;

        if (rb != null) rb.linearVelocity = Vector2.zero;

        if (anim != null) anim.SetTrigger("Die");

        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        var hp = GetComponent<PlayerHealth>();

        if (hp != null)
        {
            if (hp.currentHearts > 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
