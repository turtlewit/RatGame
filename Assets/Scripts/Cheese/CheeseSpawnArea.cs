using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CheeseSpawnArea : MonoBehaviour
{

#if UNITY_EDITOR
    [MenuItem("GameObject/RatGame/Cheese Spawn Area", false, 0)]
    static void AddCheeseSpawnAreaGameObject(MenuCommand command)
    {
        GameObject s = new GameObject("Cheese Spawn Area");
        s.AddComponent<CheeseSpawnArea>();
        GameObjectUtility.SetParentAndAlign(s, command.context as GameObject);
        Undo.RegisterCreatedObjectUndo(s, "Create " + s.name);
        Selection.activeObject = s;
    }
#endif

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
