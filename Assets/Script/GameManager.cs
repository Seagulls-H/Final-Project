using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;

    public static GameObject PlayerInstance { get; private set; }

    private static Transform spawnPoint;

    void Awake()
    {
        int selected = PlayerPrefs.GetInt("SelectedCharacter", -1);
        Debug.Log($"[GM] Loaded SelectedCharacter = {selected}");
        if (selected == -1)
            Debug.LogWarning("[GM] No index saved yet! (mặc định -1)");

        GameObject sp = GameObject.Find("SpawnPoint");
        if (sp == null)
        {
            Debug.LogError("No SpawnPoint!");
            return;
        }

        spawnPoint = sp.transform;

        if (playerPrefabs == null || playerPrefabs.Length == 0)
        {
            Debug.LogError("No playerPrefabs assigned!");
            return;
        }

        if (selected < 0 || selected >= playerPrefabs.Length)
        {
            Debug.LogWarning("[GM] Index out of range → use 0");
            selected = 0;
        }

        PlayerInstance = Instantiate(playerPrefabs[selected], spawnPoint.position, Quaternion.identity);
        Debug.Log($"[GM] Spawn prefab index = {selected} name = {playerPrefabs[selected].name}");
    }

    public static void RespawnPlayer()
    {
        if (PlayerInstance != null && spawnPoint != null)
        {
            PlayerInstance.transform.position = spawnPoint.position;

            Rigidbody2D rb = PlayerInstance.GetComponent<Rigidbody2D>();

            if (rb != null) rb.linearVelocity = Vector2.zero;

            Debug.Log("[GM] Respawn player to starting point");
        }
        else
        {
            Debug.LogWarning("[GM] Respawn fallback → reload scene");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
