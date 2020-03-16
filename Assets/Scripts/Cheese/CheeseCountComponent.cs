using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[AddComponentMenu("CheeseCount")]
public class CheeseCountComponent : NetworkBehaviour
{
    public delegate void CheeseCountChangedDelegate(GameObject player, int newCount);
    public static event CheeseCountChangedDelegate CheeseCountChanged;

    [SyncVar]
    int cheeseCount;

    public int CheeseCount 
    {
        get => cheeseCount; 
        set 
        {
            if (!isServer)
            {
                Debug.LogError($"{gameObject.name}: Attempting to change CheeseCount from client!");
                return;
            }
            cheeseCount = value;
            CheeseCountChanged(gameObject, value);
        }
    }
}
