using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This function will be called when the Play button is clicked
    public void PlayGame()
    {
        // Load the main game scene (assuming it's named "GameScene")
        SceneManager.LoadScene("Main");
    }

    // This function will be called when the Quit button is clicked
    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quit Game"); // This will show a message in the Unity editor console
        Application.Quit();
    }
}
