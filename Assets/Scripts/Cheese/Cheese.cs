using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Cheese : NetworkBehaviour
{
    [SerializeField]
    Transform objectToRotate;

    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    float initialLaunchForce;

    [SerializeField]
    Collider pickupTrigger;

    public override void OnStartServer() 
    {
        if (GetComponent<Rigidbody>() is Rigidbody rigidbody)
        {
            // First, rotate the forward vector 45 degrees upwards (axis of rotation is right). Then, rotate it randomly about the vertical axis.
            Quaternion rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up) * Quaternion.AngleAxis(-45f, Vector3.right);
            Debug.DrawRay(transform.position, rotation * Vector3.forward, Color.green, 5);
            rigidbody.AddForce((rotation * Vector3.forward) * initialLaunchForce, ForceMode.Impulse);
        }

        //pickupTrigger.enabled = false;
        //StartCoroutine(EnableTrigger());
    }

    IEnumerator EnableTrigger()
    {
        yield return new WaitForSeconds(1);
        pickupTrigger.enabled = true;
    }

    void Update()
    {
        Vector3 rotation = objectToRotate.eulerAngles;
        rotation.y += rotationSpeed * Time.deltaTime;
        objectToRotate.eulerAngles = rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasAuthority)
            return;
        if (other.GetComponent<PlayerCollider>() is PlayerCollider c && c.player.GetComponent<CheeseCountComponent>() is CheeseCountComponent cheese && c.player is NetworkPlayer player)
        {
            if (player.Stunned == true)
                return;
            ++cheese.CheeseCount;
            player.CollectCheese();
            Destroy(gameObject);
        }
    }
}
