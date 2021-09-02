using UnityEngine;

[CreateAssetMenu(fileName = "New gun", menuName = "ScriptableObjects/Gun")]
public class GunData : ScriptableObject
{
    public ParticleSystem ShotGFX;
    public AudioClip ShotSFX;
    public int Damage;
}
