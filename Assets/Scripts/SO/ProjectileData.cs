using UnityEngine;

[CreateAssetMenu(fileName = "New projectile", menuName = "ScriptableObjects/Projectile")]
public class ProjectileData : ScriptableObject
{
    public int StartRicochets;
    public AudioClip RicochetSFX;
    public AudioClip ObstacleSFX;
}
