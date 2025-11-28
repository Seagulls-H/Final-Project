using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void Exit()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
