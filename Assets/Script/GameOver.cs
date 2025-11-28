using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayAgain()
    {
        PlayerHealth.ResetHearts();  
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    public void Exit()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
