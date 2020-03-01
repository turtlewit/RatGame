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
        Debug.Log(currentPlayers);
    }

    void OnPlayerDeath()
    {
        Debug.Log("AFAFDSAFADSF!");
        --currentPlayers;
        if (currentPlayers == 1)
        {
            Debug.Log("Ayy lmao");
            NetworkManager.singleton.ServerChangeScene("Lobby");
        }
    }
}
