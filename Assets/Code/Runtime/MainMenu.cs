using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void PlayGame() {
        SceneManager.LoadScene(1);
        Debug.Log("Start button was clicked");
    }

    public void QuitGame() {
        PlayerPrefs.DeleteAll();
        Debug.Log("Quit button was clicked");
        Application.Quit();
    }
}