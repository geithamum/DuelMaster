using System;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene loading
using UnityEngine.UI; // For button interaction
using UnityEngine.XR.Interaction.Toolkit; // For XR interaction support

public class MainMenuController : MonoBehaviour
{
    // Reference to the Playground button
    public Button playgroundButton;
    public Button MultiplayerButton;
    public Button SingleplayerButton;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the button is assigned and set up the listener for the click event
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

    // This method will be called when the Playground button is clicked
    private void OnPlaygroundButtonClick()
    {
        // Load the Sample scene
        SceneManager.LoadScene("Playground");
    }
    private void OnMultiplayerButtonClick()
    {
        // Load the Sample scene
        SceneManager.LoadScene("LobbyScene");
    }
    private void OnSingleplayerButtonClick()
    {
        // Load the Sample scene
        SceneManager.LoadScene("Singleplayer/Singleplayer");
    }

}