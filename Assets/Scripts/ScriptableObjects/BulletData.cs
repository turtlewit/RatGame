using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletData : ScriptableObject
{
    public float speed;
    public GameObject hitVFX;
    public AudioClip[] clips;
}
