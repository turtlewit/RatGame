using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[AddComponentMenu("NetworkSpecificSetup")]
public class NetworkSpecificSetup : NetworkBehaviour
{
    [Header("Objects in this list will only exist locally (e.g. the camera).")]
    [SerializeField]
    Object[] deleteIfNotAuthority;

    void Start()
    {
        if (!hasAuthority)
        {
            foreach (var obj in deleteIfNotAuthority) 
            {
                Destroy(obj);
            }
        }
        Destroy(this);
    }
}
