using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField]
    float lerpFactor;

    [SerializeField]
    float mouseFactor;


    Transform localPlayer;

    void Awake()
    {
        NetworkPlayer.LocalPlayerSpawn += OnLocalPlayerSpawn;
    }

    void OnLocalPlayerSpawn(GameObject player)
    {
        localPlayer = player.transform;
    }

    void Update()
    {
        if (localPlayer == null)
            return;

        Vector3 pos = transform.position;
        pos.y = 0;

        Vector3 playerPos = localPlayer.position;
        playerPos.y = 0;

        Camera cam = GetComponent<Camera>();
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
        Vector3 offset = ((new Vector3(cam.pixelWidth, 0, cam.pixelHeight) / 2) - mousePosition) / 4.0f;
        offset *= -1;
        offset *= mouseFactor;

        offset.y = 0;

        pos = Vector3.Lerp(pos, playerPos + offset, lerpFactor * Time.deltaTime);

        pos.y = transform.position.y;

        transform.position = pos;
    }
}
