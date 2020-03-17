using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    float amount;

    [SerializeField]
    float duration;

    NetworkPlayer localPlayer;

    float currentTime;

    void Awake()
    {
        NetworkPlayer.LocalPlayerSpawn += OnLocalPlayerSpawn;
        enabled = false;
    }

    void OnDestroy()
    {
        NetworkPlayer.LocalPlayerSpawn -= OnLocalPlayerSpawn;
        localPlayer.EventPlayerHasShot -= OnPlayerHasShot;
    }

    void OnLocalPlayerSpawn(GameObject player)
    {
        if (player.GetComponent<NetworkPlayer>() is NetworkPlayer np)
        {
            localPlayer = np;
            NetworkPlayer.LocalPlayerSpawn -= OnLocalPlayerSpawn;
            np.EventPlayerHasShot += OnPlayerHasShot;
        }
    }

    void OnPlayerHasShot()
    {
        enabled = true;
        currentTime = duration;
    }

    void Update()
    {
        float factor = (amount) * (currentTime / duration);

        float x = Random.Range(-factor, factor);
        float z = Random.Range(-factor, factor);

        transform.localPosition = new Vector3(x, 0, z);


        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            enabled = false;
            transform.localPosition = Vector3.zero;
        }
    }


}
