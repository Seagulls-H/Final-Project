using UnityEngine;
using System.Collections;

public class MovingTrapPlatform : MonoBehaviour
{
    [Header("Move the background")]
    public float moveDistance = 3f;     
    public float moveSpeed = 5f;         

    [Header("Activation conditions")]
    public float triggerDistanceX = 5f;  
    public float minHeightOffset = 0.5f; 

    [Header("Spike pops up")]
    public float delayBeforeSpike = 0.2f;
    public GameObject spikeToPop;

    private bool activated = false;
    private Vector3 startPos;
    private Vector3 targetPos;

    private Transform player;

    private IEnumerator Start()
    {
        startPos = transform.position;
        targetPos = new Vector3(startPos.x + moveDistance, startPos.y, startPos.z);

        if (spikeToPop != null)
            spikeToPop.SetActive(false);

        while (player == null)
        {
            if (GameManager.PlayerInstance != null)
            {
                player = GameManager.PlayerInstance.transform;
                Debug.Log("[MovingTrapPlatform] Get player from GameManager: " + player.name);
                break;
            }

            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                player = p.transform;
                Debug.Log("[MovingTrapPlatform] Found Player by tag: " + player.name);
                break;
            }

            yield return null;
        }
    }

    void Update()
    {
        if (activated || player == null) return;

        float dx = Mathf.Abs(player.position.x - transform.position.x);

        bool playerAbove = player.position.y > transform.position.y + minHeightOffset;

        if (dx <= triggerDistanceX && playerAbove)
        {
            activated = true;
            StartCoroutine(ActivateTrap());
        }
    }

    private IEnumerator ActivateTrap()
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        yield return new WaitForSeconds(delayBeforeSpike);

        if (spikeToPop != null)
        {
            spikeToPop.SetActive(true);
        }
    }
}
