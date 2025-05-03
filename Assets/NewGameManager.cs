using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class NewGameManager : MonoBehaviour
{
    public Button resumeButton;    // Reference to the Resume button
    public Button quitButton;      // Reference to the Quit button
    public Button playgroundButton;
    public Button MultiplayerButton;
    public Button SingleplayerButton;
    public Transform head;
    public float spawnDistance = 2;
    public GameObject menu;
    public InputActionProperty showButton;

    // Start is called before the first frame update
    void Start()
    {
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
            playgroundButton.onClick.AddListener(OnPlaygroundButtonClick);
        }
        if (MultiplayerButton != null)
        {
            MultiplayerButton.onClick.AddListener(OnMultiplayerButtonClick);
        }
        if (SingleplayerButton != null)
        {
            SingleplayerButton.onClick.AddListener(OnSingleplayerButtonClick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }

    private void OnPlaygroundButtonClick()
    {
        // Load the Sample scene
        SceneManager.LoadScene("Playground");
    }
    private void OnMultiplayerButtonClick()
    {
        // Load the Sample scene
        SceneManager.LoadScene("multiplayer");
    }
    private void OnSingleplayerButtonClick()
    {
        // Load the Sample scene
        SceneManager.LoadScene("Singleplayer/Singleplayer");
    }

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
}
