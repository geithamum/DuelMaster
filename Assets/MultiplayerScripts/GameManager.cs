using Fusion;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    [Networked] public int Player1Score { get; set; }
    [Networked] public int Player2Score { get; set; }
    [Networked] public NetworkBool MatchActive { get; set; }
    [Networked] public PlayerRef LastScorer { get; set; }

    private PlayerSpawner playerSpawner;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void Spawned()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();
    }

    public void StartMatch()
    {
        if (Runner.IsServer)
        {
            MatchActive = true;
            Player1Score = 0;
            Player2Score = 0;
            RPC_AnnounceMatchStart();
        }
    }

    public void PlayerScored(PlayerRef player)
    {
        if (Runner.IsServer && MatchActive)
        {
            LastScorer = player;

            if (player.PlayerId == 1) Player1Score++;
            else Player2Score++;

            if (Player1Score >= 5 || Player2Score >= 5)
            {
                EndMatch();
            }
            else
            {
                RPC_RespawnSwords();
            }
        }
    }

    private void EndMatch()
    {
        MatchActive = false;
        RPC_AnnounceWinner();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_AnnounceMatchStart()
    {
        Debug.Log("Match started!");
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_AnnounceWinner()
    {
        string winner = Player1Score >= 5 ? "Player 1" : "Player 2";
        Debug.Log($"{winner} wins the match!");
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_RespawnSwords()
    {
        if (playerSpawner != null)
        {
            playerSpawner.RespawnAllSwords();
        }
    }
}