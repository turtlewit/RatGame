using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ReShowGUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManagerHUD nm = NetworkRoomManager.singleton.GetComponent<NetworkManagerHUD>();
        nm.showGUI = true;
    }
}
