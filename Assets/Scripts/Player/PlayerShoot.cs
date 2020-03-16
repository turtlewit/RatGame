using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    float disableShootSeconds;

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

    void OnPlayerShot()
    {
        enabled = false;
        if (GetComponent<LookAtCursor>() is LookAtCursor c)
        {
            c.enabled = false;
        }
        StartCoroutine(EnableMovement());
    }

    IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(disableShootSeconds);
        if (GetComponent<LookAtCursor>() is LookAtCursor c)
        {
            c.enabled = true;
        }
        enabled = true;
    }
}
