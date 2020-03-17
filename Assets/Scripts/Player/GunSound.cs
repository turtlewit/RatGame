using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSound : MonoBehaviour
{
    [SerializeField]
    AudioSource sound;

    void Start()
    {
        if (GetComponent<NetworkPlayer>() is NetworkPlayer player)
        {
            player.EventPlayerHasShot += OnPlayerHasShot;
        }
    }

    void OnPlayerHasShot()
    {
        sound.PlayOneShot(sound.clip);
    }
}
