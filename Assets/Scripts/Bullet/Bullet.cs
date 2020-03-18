using System.Collections;
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
        if (!hasAuthority || ignore == null || (other.GetComponent<PlayerCollider>() != null && other.GetComponent<PlayerCollider>().player.gameObject == ignore))
        {
            return;
        }

        if (other.GetComponent<PlayerCollider>() != null && other.GetComponent<PlayerCollider>().player is NetworkPlayer player)
        {
            player.Shot();
        }
        else
        {
            WallHitAudio.Singleton.Play(transform.position);
        }

        var particles = Instantiate(data.hitVFX, transform.position, Quaternion.AngleAxis(-90, Vector3.right));
        NetworkServer.Spawn(particles);
        Destroy(particles, 1);


        Destroy(gameObject);
    }
}
