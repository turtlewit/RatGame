using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WallHitAudio : NetworkBehaviour 
{
    public static WallHitAudio Singleton;

    [SerializeField]
    AudioClip[] clips;

    void Awake()
    {
        Singleton = this;
    }

    [Server]
    public void Play(Vector3 location)
    {
        RpcPlay(location);
    }

    [ClientRpc]
    void RpcPlay(Vector3 location)
    {
        AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], location);
    }

}
