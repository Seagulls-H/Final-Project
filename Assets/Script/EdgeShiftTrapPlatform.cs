using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EdgeShiftTrapPlatform : MonoBehaviour
{
    [Header("Move the background")]
    public float shiftDistance = 2f;      
    public float moveSpeed = 6f;          

    [Header("Activation conditions")]
    public float triggerMarginX = 2.5f;   
    public float maxVerticalDiff = 1.0f;  

    private Transform player;
    private BoxCollider2D col;
    private bool activated = false;

    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        startPos = transform.position;
        targetPos = startPos + Vector3.left * shiftDistance;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
        else
        {
            Debug.LogWarning("EdgeShiftTrapPlatform: Player not found (tag 'Player').");
        }
    }

    void Update()
    {
        if (activated || player == null) return;

        float groundRight = col.bounds.max.x;
        float dx = groundRight - player.position.x;  
        bool nearRightEdge = dx >= -0.1f && dx <= triggerMarginX;
        float groundTop = col.bounds.max.y;
        float dy = Mathf.Abs(player.position.y - groundTop);
        bool similarHeight = dy <= maxVerticalDiff;

        if (nearRightEdge && similarHeight)
        {
            activated = true;
            StartCoroutine(MoveLeft());
        }
    }

    System.Collections.IEnumerator MoveLeft()
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
}
