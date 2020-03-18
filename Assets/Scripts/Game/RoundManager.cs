using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoundManager : NetworkBehaviour
{
    public delegate void PlayerScoreChangedDelegate(int player, int newScore);
    public delegate void GameWonDelegate(int player);

    [SyncEvent]
    private event PlayerScoreChangedDelegate EventPlayerScoreChanged;

    [SyncEvent]
    private event GameWonDelegate EventGameWon;

    public static event PlayerScoreChangedDelegate PlayerScoreChanged 
    {
        add { Singleton.EventPlayerScoreChanged += value; }
        remove { Singleton.EventPlayerScoreChanged -= value; }
    }

    public static event GameWonDelegate GameWon
    {
        add { Singleton.EventGameWon += value; }
        remove { Singleton.EventGameWon -= value; }
    }

    public static RoundManager Singleton { get; private set; }

    public static int Winner { get; private set; } = -1;

    [SerializeField]
    GameRules rules;

    Dictionary<GameObject, int> players = new Dictionary<GameObject, int>();

    void Awake()
    {
        Singleton = this;
    }

    public int GetTeam(GameObject obj)
    {
        return players[obj];
    }

    public override void OnStartServer()
    {
        // Must listen for events here because network behaviour has not been
        // initialized when OnEnable is called. But we still need to handle enable
        // and disable.
        CheeseCountComponent.CheeseCountChanged += OnCheeseCountChanged;
        NetworkPlayer.PlayerSpawn += OnPlayerSpawn;
    }

    void OnEnable()
    {
        if (!isServer)
            return;
        CheeseCountComponent.CheeseCountChanged += OnCheeseCountChanged;
        NetworkPlayer.PlayerSpawn += OnPlayerSpawn;
    }

    void OnDisable()
    {
        if (!isServer)
            return;
        CheeseCountComponent.CheeseCountChanged -= OnCheeseCountChanged;
        NetworkPlayer.PlayerSpawn -= OnPlayerSpawn;
    }

    [Server]
    void OnCheeseCountChanged(GameObject player, int cheeseCount)
    {
        EventPlayerScoreChanged(players[player], cheeseCount);
        if (cheeseCount >= rules.CheeseCountToWin) 
        {
            Debug.Log($"Player {players[player]} won");
            Winner = players[player];
            EventGameWon(players[player]);
        }
    }

    [Server]
    void OnPlayerSpawn(GameObject player)
    {
        if (player.GetComponent<NetworkPlayer>() is NetworkPlayer np)
        {
            int n = players.Count;
            np.SetPlayerNumber(n);
            players[player] = n;
        }
        else
        {
            Debug.Log($"OnPlayerSpawn argument player {player} has no NetworkPlayer component. This is pretty much impossible.");
        }
    }
}
