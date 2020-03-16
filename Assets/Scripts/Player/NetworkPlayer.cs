using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[AddComponentMenu("Player/NetworkPlayer")]
public class NetworkPlayer : NetworkBehaviour
{
    public delegate void PlayerSpawnDelegate(GameObject player);
    public static event PlayerSpawnDelegate PlayerSpawn;

    public delegate void PlayerShotDelegate();
    [SyncEvent]
    public event PlayerShotDelegate EventPlayerShot;


    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject cheesePrefab;

    [SerializeField]
    Transform bulletSpawnPosition;

    [SyncVar]
    int playerNumber;

    public int PlayerNumber { get => playerNumber; set {} }

    void Start()
    {
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

        EventPlayerShot?.Invoke();
    }

}
