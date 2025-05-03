using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;

public class NetworkConnect : MonoBehaviour
{
    public TMP_InputField ipInput;  // Input field for the IP address
    public GameObject playerPrefab; // Player prefab to spawn
    public GameObject swordPrefab;  // Sword prefab to spawn

    // Spawn points for the players and swords
    public Transform player1Spawn;
    public Transform player2Spawn;
    public Transform sword1Spawn;
    public Transform sword2Spawn;

    private GameObject player1;
    private GameObject player2;
    private GameObject sword1;
    private GameObject sword2;

    // Start host (server)
    public void Create()
    {
        // Start the server in host mode
        NetworkManager.Singleton.StartHost();
        Debug.Log("Host started");

        // Subscribe to the client connected event
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    // Join by entered IP
    public void Join()
    {
        string ipAddress = ipInput.text;

        // Save the IP address entered by the user
        PlayerPrefs.SetString("LastIP", ipAddress);
        PlayerPrefs.Save();

        var transport = NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();
        if (transport != null)
        {
            transport.ConnectionData.Address = ipAddress;
            transport.ConnectionData.Port = 7777; // Ensure this port matches the server
        }
        else
        {
            Debug.LogError("UnityTransport not found on NetworkManager!");
            return;
        }

        // Start the client and try to connect to the server
        NetworkManager.Singleton.StartClient();
        Debug.Log("Connecting to: " + ipAddress);
    }

    // Join local IP (for LAN play)
    public void JoinLan()
    {
        var transport = NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();
        if (transport != null)
        {
            transport.ConnectionData.Address = "127.0.0.1"; // Local IP for LAN play
            transport.ConnectionData.Port = 7777; // Ensure this port matches the server
        }
        else
        {
            Debug.LogError("UnityTransport not found on NetworkManager!");
            return;
        }

        // Start the client
        NetworkManager.Singleton.StartClient();
        Debug.Log("Connecting to local server: 127.0.0.1");
    }

    // Called when a client connects to the server
    private void OnClientConnected(ulong clientId)
    {
        Debug.Log("Client connected with ID: " + clientId);

        // Ensure that we are only spawning players and swords when the second player connects
        if (NetworkManager.Singleton.IsServer && NetworkManager.Singleton.ConnectedClients.Count == 2)
        {
            SpawnPlayersAndSwords();
        }
    }

    // Method to spawn players and swords
    private void SpawnPlayersAndSwords()
    {
        int clientIndex = 0;

        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            Vector3 spawnPos = clientIndex == 0 ? player1Spawn.position : player2Spawn.position;
            Quaternion spawnRot = clientIndex == 0 ? player1Spawn.rotation : player2Spawn.rotation;

            GameObject player = Instantiate(playerPrefab, spawnPos, spawnRot);
            var netObj = player.GetComponent<NetworkObject>();
            netObj.SpawnAsPlayerObject(client.ClientId);  // This ensures the client "owns" their own object

            // Spawn and attach a sword
            Vector3 swordPos = clientIndex == 0 ? sword1Spawn.position : sword2Spawn.position;
            Quaternion swordRot = clientIndex == 0 ? sword1Spawn.rotation : sword2Spawn.rotation;

            GameObject sword = Instantiate(swordPrefab, swordPos, swordRot);
            sword.GetComponent<NetworkObject>().SpawnWithOwnership(client.ClientId); // Optional: give them sword ownership
            sword.transform.SetParent(player.transform);

            clientIndex++;
        }

        Debug.Log("Spawned players and swords per client.");
    }

}
