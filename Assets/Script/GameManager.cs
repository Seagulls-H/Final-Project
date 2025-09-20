using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Prefabs (index phải khớp với UI)")]
    public GameObject[] playerPrefabs;

    private int selectedCharacterIndex = 0;
    private GameObject currentPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("GameManager Awake - tạo instance");
        }
        else
        {
            Debug.Log("GameManager Awake - destroy duplicate");
            Destroy(gameObject);
        }
    }

    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
        PlayerPrefs.SetInt("SelectedCharacter", index); 
        PlayerPrefs.Save();
        Debug.Log("GameManager: SelectCharacter = " + index);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if (scene.name == "Gameplay")
        {
            // lấy lại từ PlayerPrefs nếu cần
            selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", selectedCharacterIndex);
            SpawnPlayerInScene();
        }
    }

    private void SpawnPlayerInScene()
    {
        // 🔥 Tìm object tên "SpawnPoint" trong scene Gameplay
        GameObject sp = GameObject.Find("SpawnPoint");

        if (sp == null)
        {
            Debug.LogError("GameManager: Không tìm thấy SpawnPoint trong Gameplay! Hãy tạo Empty tên 'SpawnPoint' tại vị trí spawn.");
            return;
        }

        if (playerPrefabs == null || playerPrefabs.Length == 0)
        {
            Debug.LogError("GameManager: playerPrefabs chưa gán trong inspector!");
            return;
        }

        if (selectedCharacterIndex < 0 || selectedCharacterIndex >= playerPrefabs.Length)
        {
            Debug.LogWarning("GameManager: index out of range, dùng 0");
            selectedCharacterIndex = 0;
        }

        // nếu đã có player cũ thì destroy để tránh duplicate
        if (currentPlayer != null) Destroy(currentPlayer);

        currentPlayer = Instantiate(playerPrefabs[selectedCharacterIndex], sp.transform.position, Quaternion.identity);
        Debug.Log("GameManager: Spawned player index = " + selectedCharacterIndex + " prefab = " + playerPrefabs[selectedCharacterIndex].name);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
