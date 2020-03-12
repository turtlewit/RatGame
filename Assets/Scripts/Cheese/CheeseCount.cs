using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[AddComponentMenu("CheeseCount")]
public class CheeseCount : NetworkBehaviour
{
    [SyncVar]
    public int cheeseCount;
}
