using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Cheese : NetworkBehaviour
{
    [SerializeField]
    float rotationSpeed;

    public override void OnStartServer() 
    {
        if (GetComponent<Rigidbody>() is Rigidbody rigidbody)
        {
            rigidbody.angularVelocity = new Vector3(0, rotationSpeed, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CheeseCount>() is CheeseCount cheese)
        {
            ++cheese.cheeseCount;
            Destroy(gameObject);
        }
    }
}
