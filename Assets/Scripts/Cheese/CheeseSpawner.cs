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

    [SerializeField]
    int maxCheeseOnMap;

    [SerializeField]
    float spawnInterval;

    float currentTime = 0;

    void Start()
    {
        for (int i = 0; i < maxCheeseOnMap; ++i)
            SpawnCheese();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > spawnInterval)
        {
            currentTime -= spawnInterval;
            
            int cheeseCount = FindObjectsOfType<Cheese>().Length;

            if (cheeseCount < maxCheeseOnMap)
                SpawnCheese();
        }
    }

    void SpawnCheese()
    {
        var SpawnAreas = GetComponentsInChildren<CheeseSpawnArea>();
        CheeseSpawnArea chosenArea = SpawnAreas[Random.Range(0, SpawnAreas.Length)];
        Vector3 spawnPosition = chosenArea.GetRandomPosition();

        var Cheese = Instantiate(prefab, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(Cheese);
    }

}
