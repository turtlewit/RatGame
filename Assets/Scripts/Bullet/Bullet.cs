﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Bullet : NetworkBehaviour 
{
    [SerializeField]
    BulletData data;

    [SerializeField]
    Rigidbody body;

    [SyncVar]
    GameObject ignore;

    // Start is called before the first frame update
    void Start()
    {
        body.velocity = transform.forward * data.speed;
    }

    [Server]
    public void SetIgnore(GameObject ignore)
    {
        this.ignore = ignore;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasAuthority || ignore == null || other.gameObject == ignore)
        {
            Debug.Log($"Colliding {other.name}");
            return;
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
