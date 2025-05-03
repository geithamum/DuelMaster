using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class GameManagerForPlayground : MonoBehaviour
{
    public GameObject pauseMenuPrefab; // Reference to the PauseMenu prefab
    private GameObject pauseMenuInstance; // To hold the instantiated PauseMenu
    public Transform playerCamera;

    private bool isPaused = false;

    void Update()
    {
        // Check for W key press to trigger the pause menu
        if (Input.GetKeyDown(KeyCode.W) && !isPaused)
        {
            // Instantiate the PauseMenu prefab and parent it to the current scene
            pauseMenuInstance = Instantiate(pauseMenuPrefab, Vector3.zero, Quaternion.identity);

            // Optionally, set the position of the PauseMenu to be in front of the player in VR
            // If it's a VR game, you'll probably want to place the menu in front of the camera
            // Example (if you have a VR camera):
            pauseMenuInstance.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5f;

            // Set the pause flag to true
            isPaused = true;

            // Pause the game
            Time.timeScale = 0;
        }
    }

    // Method to unpause the game and remove the PauseMenu
    public void ResumeGame()
    {
        // Destroy the pause menu instance
        if (pauseMenuInstance != null)
        {
            Destroy(pauseMenuInstance);
        }

        // Unpause the game
        Time.timeScale = 1;
        isPaused = false;
    }

    // Add any other methods you need for quit or scene loading here
}