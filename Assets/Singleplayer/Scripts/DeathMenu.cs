using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;



public class DeathMenu : MonoBehaviour
{
    public Button mainMenu;
    public Button tryAgain;

    // Start is called before the first frame update
    void Start()
    {
        if (mainMenu != null)
        {
            mainMenu.onClick.AddListener(MainMenu);
        }
        if (tryAgain != null)
        {
            tryAgain.onClick.AddListener(TryAgain);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MainMenu()
    {
        SceneManager.LoadScene("UI");
    }

    void TryAgain()
    {
        SceneManager.LoadScene("Singleplayer");
    }
}
