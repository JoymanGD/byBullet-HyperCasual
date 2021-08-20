using UnityEngine;

[CreateAssetMenu(fileName = "New gun", menuName = "ScriptableObjects/Gun")]
public class GunData : ScriptableObject
{
    public ProjectileBehaviour Projectile;
    public int Damage;
}
