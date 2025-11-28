using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Hearts / Lives")]
    public int maxHearts = 3;
    public int currentHearts;

    private static int sharedHearts = -1;

    private HeartsUI heartsUI;
    private bool isHandlingDeath = false; 

    private void Start()
    {
        heartsUI = FindAnyObjectByType<HeartsUI>();

        if (sharedHearts < 0)
        {
            sharedHearts = maxHearts;
        }

        currentHearts = sharedHearts;

        if (heartsUI != null)
        {
            heartsUI.SetupHearts(maxHearts);
            heartsUI.UpdateHearts(currentHearts);
        }
    }

    public void OnPlayerKilled()
    {
        if (isHandlingDeath) return;
        isHandlingDeath = true;

        sharedHearts--;
        if (sharedHearts < 0) sharedHearts = 0;

        currentHearts = sharedHearts;

        if (heartsUI != null)
        {
            heartsUI.UpdateHearts(currentHearts);
        }

        var death = GetComponent<PlayerDeath>();
        if (death != null)
        {
            death.Die();
        }
        else
        {
            Debug.LogWarning("PlayerHealth: No PlayerDeath found on Player.");
        }
    }

    public static void ResetHearts()
    {
        sharedHearts = -1;
    }
}
