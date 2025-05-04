using Fusion;
using UnityEngine;
using System.Collections.Generic;
using Fusion.Sockets;
using System;
using System.Linq; // Required for Count()

public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef xrPlayerPrefab;
    [SerializeField] private NetworkPrefabRef swordPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private Dictionary<PlayerRef, NetworkObject> spawnedPlayers = new Dictionary<PlayerRef, NetworkObject>();
    private Dictionary<PlayerRef, NetworkObject> spawnedSwords = new Dictionary<PlayerRef, NetworkObject>();

    private void Start()
    {
        if (NetworkManager.Instance != null)
        {
            NetworkManager.Instance.Runner.AddCallbacks(this);
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            SpawnPlayer(runner, player);
            SpawnSword(runner, player);

            // Use LINQ Count() because ActivePlayers is IEnumerable<PlayerRef>
            if (runner.ActivePlayers.Count() == 2 && GameManager.Instance != null)
            {
                GameManager.Instance.StartMatch();
            }
        }
    }

    private void SpawnPlayer(NetworkRunner runner, PlayerRef player)
    {
        int spawnIndex = spawnedPlayers.Count % spawnPoints.Length;
        Vector3 position = spawnPoints[spawnIndex].position;
        Quaternion rotation = spawnPoints[spawnIndex].rotation;

        NetworkObject playerObj = runner.Spawn(
            xrPlayerPrefab,
            position,
            rotation,
            player
        );
        spawnedPlayers.Add(player, playerObj);
    }

    private void SpawnSword(NetworkRunner runner, PlayerRef player)
    {
        int spawnIndex = player.PlayerId % spawnPoints.Length;
        Vector3 position = spawnPoints[spawnIndex].position + spawnPoints[spawnIndex].forward * 1f;

        NetworkObject swordObj = runner.Spawn(
            swordPrefab,
            position,
            Quaternion.identity
        );
        spawnedSwords.Add(player, swordObj);
    }

    public void RespawnAllSwords()
    {
        var runner = NetworkManager.Instance?.Runner;
        if (runner == null || !runner.IsServer) return;

        foreach (var pair in spawnedSwords)
        {
            runner.Despawn(pair.Value);
            SpawnSword(runner, pair.Key);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (spawnedPlayers.TryGetValue(player, out NetworkObject playerObj))
        {
            runner.Despawn(playerObj);
            spawnedPlayers.Remove(player);
        }

        if (spawnedSwords.TryGetValue(player, out NetworkObject swordObj))
        {
            runner.Despawn(swordObj);
            spawnedSwords.Remove(player);
        }
    }

    // Required INetworkRunnerCallbacks implementations
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
}
