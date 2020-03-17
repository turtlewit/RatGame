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

    void OnEnable()
    {
        if (GetComponent<NetworkPlayer>() is NetworkPlayer player)
        {
            player.EventPlayerShot += OnPlayerShot;
        }
    }

    void OnDisable()
    {

        if (GetComponent<NetworkPlayer>() is NetworkPlayer player)
        {
            player.EventPlayerShot -= OnPlayerShot;
        }
    }

    void OnPlayerShot(float seconds)
    {
        enabled = false;
        if (GetComponent<LookAtCursor>() is LookAtCursor c)
        {
            c.enabled = false;
        }
        StartCoroutine(EnableMovement(seconds));
    }

    IEnumerator EnableMovement(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (GetComponent<LookAtCursor>() is LookAtCursor c)
        {
            c.enabled = true;
        }
        enabled = true;
    }
}
