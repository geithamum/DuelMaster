using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using UnityEngine.UI;  // For Button components
using UnityEngine.XR.Interaction.Toolkit; // For VR interaction with buttons

public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;    // Reference to the Resume button
    public Button quitButton;      // Reference to the Quit button
    public Button playgroundButton; // Reference to the Playground button
    public Button multiplayerButton; // Reference to the Multiplayer button

    void Start()
    {
        // Ensure the buttons are set up to call the correct methods when clicked
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame); // Add listener for Resume button
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame); // Add listener for Quit button
        }

        if (playgroundButton != null)
        {
            playgroundButton.onClick.AddListener(LoadPlaygroundScene); // Add listener for Playground button
        }

        if (multiplayerButton != null)
        {
            multiplayerButton.onClick.AddListener(LoadMultiplayerScene); // Add listener for Multiplayer button
        }
    }

    // Method to be called when the Resume button is clicked
    void ResumeGame()
    {
        // Unpause the game and hide the PauseMenu
        Debug.Log("Resumed");
        Time.timeScale = 1;  // Unpause the game

        // Unload the PauseMenu scene
        SceneManager.UnloadSceneAsync("PauseMenu");
    }

    // Method to be called when the Quit button is clicked
    void QuitGame()
    {
        // Print "Quit" to the Unity console for debugging purposes
        Debug.Log("Quit");

        // Close the game (this will stop the play mode in the Unity Editor, or quit in a built application)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops the game in the editor
#else
            Application.Quit(); // Quit the game in a built application
#endif
    }

    // Method to load the Playground scene when the Playground button is clicked
    void LoadPlaygroundScene()
    {
        Debug.Log("Loading Playground Scene");
        SceneManager.LoadScene("Playground"); // Replace with the actual name of your Playground scene
    }

    // Method to load the Multiplayer scene when the Multiplayer button is clicked
    void LoadMultiplayerScene()
    {
        Debug.Log("Loading Multiplayer Scene");
        SceneManager.LoadScene("Multiplayer"); // Replace with the actual name of your Multiplayer scene
    }
}