using UnityEngine;

public class ProximitySpike : MonoBehaviour
{
    public float triggerDistance = 3f; 
    public GameObject spikeObject;     
    public bool triggerOnce = true;   

    private Transform player;
    private bool activated = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (spikeObject != null)
            spikeObject.SetActive(false);
    }

    void Update()
    {
        if (activated && triggerOnce) return;

        if (Vector2.Distance(player.position, transform.position) <= triggerDistance)
        {
            activated = true;

            if (spikeObject != null)
                spikeObject.SetActive(true);
        }
    }
}
