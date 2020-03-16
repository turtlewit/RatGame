using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Player/PlayerMovement")]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    // Number of seconds to disable movement for when player is shot.
    [SerializeField]
    float disableMovementSeconds;

    [SerializeField]
    Rigidbody body;

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
        StartCoroutine(EnableMovement());
    }

    IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(disableMovementSeconds);
        enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        float m = direction.magnitude;
        direction = direction.normalized;

        body.velocity = direction * speed * m;
    }
}
