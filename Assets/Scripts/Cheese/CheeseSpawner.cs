using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CheeseSpawner : NetworkBehaviour
{

#if UNITY_EDITOR
    [MenuItem("GameObject/RatGame/Cheese Spawner", false, 0)]
    static void AddCheeseSpawnerGameObject(MenuCommand command)
    {
        GameObject s = new GameObject("Cheese Spawner");
        s.AddComponent<CheeseSpawner>();
        GameObjectUtility.SetParentAndAlign(s, command.context as GameObject);
        Undo.RegisterCreatedObjectUndo(s, "Create " + s.name);
        Selection.activeObject = s;
    }
#endif

    [SerializeField]
    GameObject prefab;

}
