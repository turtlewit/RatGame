using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[AddComponentMenu("Player/NetworkPlayer")]
public class NetworkPlayer : NetworkBehaviour
{
    [Header("Objects in this list will only exist locally (e.g. the camera).")]
    [SerializeField]
    List<Object> LocalOnly = new List<Object>();

    void Start()
    {
        if (!hasAuthority)
        {
            foreach (var obj in LocalOnly)
                Destroy(obj);
            
            if (NetworkManager.singleton is NetworkManager manager)
            {
                if (manager.GetComponent<NetworkManagerHUD>() is NetworkManagerHUD hud)
                {
                    hud.enabled = false;
                }
            }
        }
    }
}
