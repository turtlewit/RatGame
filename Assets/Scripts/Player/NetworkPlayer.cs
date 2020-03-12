using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[AddComponentMenu("Player/NetworkPlayer")]
public class NetworkPlayer : NetworkBehaviour
{
    public delegate void PlayerDeathDelegate();
    public static PlayerDeathDelegate PlayerDeath;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform bulletSpawnPosition;

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

    public override void OnNetworkDestroy()
    {
        if (!isServer)
            return;
        PlayerDeath?.Invoke();
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
}
