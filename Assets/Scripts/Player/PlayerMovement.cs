using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Player/PlayerMovement")]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    Rigidbody body;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        float m = direction.magnitude;
        direction = direction.normalized;

        body.velocity = direction * speed * m;

        //transform.Translate(direction * speed * Time.deltaTime);
    }
}
