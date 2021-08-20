using UnityEngine;

[CreateAssetMenu(fileName = "New projectile", menuName = "ScriptableObjects/Projectile")]
public class ProjectileData : ScriptableObject
{
    public float StartSpeedModifier;
    public int StartRicochets;
}
