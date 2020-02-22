using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Player/PlayerMovement")]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = direction.normalized;

        transform.Translate(direction * speed * Time.deltaTime);
    }
}
