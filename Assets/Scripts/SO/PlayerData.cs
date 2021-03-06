using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player", fileName = "NewPlayer")]
public class PlayerData : ScriptableObject
{
    public AudioClip DamageGetSFX;
    public int Health;
}
