using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    private int selectedCharacter = 0;

    public void SelectCharacter(int index)
    {
        selectedCharacter = index;
        Debug.Log("✅ Đã chọn nhân vật (UI): " + index);

        // Cập nhật vào GameManager (nếu đã tồn tại)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SelectCharacter(index);
            Debug.Log("➡️ Gửi index tới GameManager");
        }
    }

    public void ConfirmSelection()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        PlayerPrefs.Save();
        Debug.Log("➡️ Lưu nhân vật vào PlayerPrefs: " + selectedCharacter);

        SceneManager.LoadScene("Gameplay"); // tên scene phải chính xác
    }
}
