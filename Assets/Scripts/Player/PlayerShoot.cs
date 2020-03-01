using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    NetworkPlayer player;

    void Start()
    {
        player = GetComponent<NetworkPlayer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && player is NetworkPlayer p)
        {
            p.CmdSpawnBullet();
        }
    }
}
