using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoundManager : MonoBehaviour
{
    int currentPlayers;

    void Start()
    {
        NetworkPlayer[] players = FindObjectsOfType<NetworkPlayer>();
        currentPlayers = players.Length;
        NetworkPlayer.PlayerDeath += OnPlayerDeath;
    }

    void OnPlayerDeath()
    {
        --currentPlayers;
        if (currentPlayers == 1)
        {
            NetworkManager.singleton.ServerChangeScene("Lobby");
        }
    }
}
