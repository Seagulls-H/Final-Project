using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    [Header("UI Display")]
    public Image characterDisplay;         
    public Sprite[] characterSprites;      

    private int currentIndex = 0;

    void Start()
    {
        ShowCharacter(currentIndex);
    }

    public void NextCharacter()
    {
        currentIndex++;
        if (currentIndex >= characterSprites.Length) currentIndex = 0;
        ShowCharacter(currentIndex);
    }

    public void PrevCharacter()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = characterSprites.Length - 1;
        ShowCharacter(currentIndex);
    }

    public void ConfirmSelection()
    {
        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);
        PlayerPrefs.Save();

        Debug.Log("✅ Selected character: " + currentIndex);
        SceneManager.LoadScene("Gameplay");
    }

    void ShowCharacter(int index)
    {
        if (characterSprites != null && characterSprites.Length > 0)
        {
            characterDisplay.sprite = characterSprites[index];
        }
    }
}
