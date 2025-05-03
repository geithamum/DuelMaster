using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public GameObject playerPrefab;
    public GameObject swordPrefab;

    public Vector3 player1Spawn = new Vector3(-0.5f, 0f, 0f);
    public Vector3 player2Spawn = new Vector3(0.5f, 0f, 0f);
    public float swordDistance = 1f;

    private ulong[] connectedClients = new ulong[2];
    private int connectedCount = 0;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Debug.Log("GameManager is now running as Server.");
            NetworkManager.OnClientConnectedCallback += OnClientConnected;
        }
    }


    private void OnDestroy()
    {
        if (IsServer)
        {
            NetworkManager.OnClientConnectedCallback -= OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (connectedCount >= 2) return; // Only allow 2 players

        connectedClients[connectedCount] = clientId;
        connectedCount++;

        if (connectedCount == 2)
        {
            SpawnPlayers();
        }
    }

    private void SpawnPlayers()
    {
        // Player 1
        GameObject player1 = Instantiate(playerPrefab, player1Spawn, Quaternion.LookRotation(Vector3.right));
        player1.GetComponent<NetworkObject>().SpawnAsPlayerObject(connectedClients[0]);

        // Player 2
        GameObject player2 = Instantiate(playerPrefab, player2Spawn, Quaternion.LookRotation(Vector3.left));
        player2.GetComponent<NetworkObject>().SpawnAsPlayerObject(connectedClients[1]);

        // Spawn swords
        Vector3 sword1Pos = player1Spawn + Vector3.forward * swordDistance;
        Vector3 sword2Pos = player2Spawn + Vector3.forward * swordDistance;

        GameObject sword1 = Instantiate(swordPrefab, sword1Pos, Quaternion.identity);
        sword1.GetComponent<NetworkObject>().Spawn();

        GameObject sword2 = Instantiate(swordPrefab, sword2Pos, Quaternion.identity);
        sword2.GetComponent<NetworkObject>().Spawn();
    }
}
