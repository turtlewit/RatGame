using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[AddComponentMenu("Player/NetworkPlayer")]
public class NetworkPlayer : NetworkBehaviour
{
    public delegate void PlayerSpawnDelegate(GameObject player);
    public delegate void PlayerShotDelegate(float seconds);
    public delegate void PlayerHasShotDelegate();
    public delegate void PlayerCollectedCheeseDelegate();


    public static event PlayerSpawnDelegate PlayerSpawn;
    public static event PlayerSpawnDelegate LocalPlayerSpawn;

    [SyncEvent]
    public event PlayerShotDelegate EventPlayerShot;

    [SyncEvent]
    public event PlayerHasShotDelegate EventPlayerHasShot;

    [SyncEvent]
    public event PlayerCollectedCheeseDelegate EventPlayerCollectedCheese;


    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject cheesePrefab;

    [SerializeField]
    Transform bulletSpawnPosition;

    [SerializeField]
    float stunnedSeconds;

    [SerializeField]
    TeamColors colors;

    [SerializeField]
    MeshRenderer render;

    [SyncVar]
    int playerNumber;

    [HideInInspector]
    [SyncVar]
    public bool Stunned = false;

    public int PlayerNumber { get => playerNumber; set {} }

    void Start()
    {
        if (isServer)
        {
            Material m = render.material;
            int team = RoundManager.Singleton.GetTeam(gameObject);
            m.color = colors.colors[team];
            RpcSetMaterial(team);
        }
        if (!hasAuthority)
        {
            if (NetworkManager.singleton is NetworkManager manager)
            {
                if (manager.GetComponent<NetworkManagerHUD>() is NetworkManagerHUD hud)
                {
                    hud.enabled = false;
                }
            }
        }
    }

    [ClientRpc]
    void RpcSetMaterial(int team)
    {
        Debug.Log("Here!");
        Material m = render.material;
        m.color = colors.colors[team];
    }

    public override void OnStartAuthority()
    {
        LocalPlayerSpawn?.Invoke(gameObject);
    }

    public override void OnStartServer()
    {
        PlayerSpawn(gameObject);
    }

    public override void OnNetworkDestroy()
    {
        if (!isServer)
            return;
    }

    [Command]
    public void CmdSpawnBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
        if (bullet.GetComponent<Bullet>() is Bullet b)
        {
            b.SetIgnore(gameObject);
        }
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 10f);
        EventPlayerHasShot?.Invoke();
    }

    [Server]
    public void SetPlayerNumber(int to)
    {
        playerNumber = to;
    }

    [Server]
    public void Shot()
    {
        if (GetComponent<CheeseCountComponent>() is CheeseCountComponent cheese)
        {
            int cheeseLost = cheese.CheeseCount / 2;
            cheese.CheeseCount -= cheeseLost;
            for (int i = 0; i < cheeseLost; ++i)
            {
                var newCheese = Instantiate(cheesePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                NetworkServer.Spawn(newCheese);
            }
        }
        else
        {
            Debug.LogError($"{gameObject.name}: Player has not CheeseCountComponent!");
        }

        SetStunned(stunnedSeconds);
        EventPlayerShot?.Invoke(stunnedSeconds);
    }

    IEnumerator SetStunned(float seconds)
    {
        Stunned = true;
        yield return new WaitForSeconds(seconds);
        Stunned = false;
    }

    [Server]
    public void CollectCheese()
    {
        EventPlayerCollectedCheese?.Invoke();
    }

}
