using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    [SerializeField]
    AudioSource sound;

    void Start()
    {
        if (GetComponent<NetworkPlayer>() is NetworkPlayer player)
        {
            player.EventPlayerShot += OnPlayerShot;
        }
    }

    void OnPlayerShot(float seconds)
    {
        sound.PlayOneShot(sound.clip);
    }
}
