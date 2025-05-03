using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // This could be used to show different UI states if necessary
    public void OnStartGame()
    {
        // Start the host
        NetworkConnect networkConnect = FindObjectOfType<NetworkConnect>();
        networkConnect.CreateHost();
    }

    public void OnJoinGame()
    {
        // Join a game with the provided IP
        NetworkConnect networkConnect = FindObjectOfType<NetworkConnect>();
        networkConnect.Join();
    }

    public void OnJoinLanGame()
    {
        // Join a LAN game (local play)
        NetworkConnect networkConnect = FindObjectOfType<NetworkConnect>();
        networkConnect.JoinLan();
    }
}
