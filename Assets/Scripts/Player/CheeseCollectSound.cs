using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseCollectSound : MonoBehaviour
{
    [SerializeField]
    AudioSource sound;

    void Start()
    {
        if (GetComponent<NetworkPlayer>() is NetworkPlayer player)
        {
            player.EventPlayerCollectedCheese += OnCollectCheese;
        }
    }

    void OnCollectCheese()
    {
        sound.PlayOneShot(sound.clip);
    }

}
