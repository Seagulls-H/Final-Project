using UnityEngine;

public class TimedMovingPlatform : MonoBehaviour
{
    [Header("Waiting time before running")]
    public float delayBeforeMove = 0.5f;

    [Header("Move")]
    public float moveSpeed = 8f;      
    public float moveDistance = 10f;   
    public bool moveToRight = true;   

    private bool activated = false;
    private bool moving = false;
    private float timer = 0f;

    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        startPos = transform.position;
        Vector3 dir = moveToRight ? Vector3.right : Vector3.left;
        targetPos = startPos + dir * moveDistance;
    }

    void Update()
    {
        if (activated && !moving)
        {
            timer += Time.deltaTime;
            if (timer >= delayBeforeMove)
            {
                moving = true;
            }
        }

        if (!moving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            moving = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (activated) return;

        if (collision.collider.CompareTag("Player"))
        {
            activated = true; 
        }
    }
}
