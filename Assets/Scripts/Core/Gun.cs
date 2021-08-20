using UnityEngine;

public class Gun : MonoBehaviour, IShooting
{
    [SerializeField] Animator Animator;
    [SerializeField] ParticleSystem FireFX;
    [SerializeField] GunData Data;
    [SerializeField] Transform Barrel;
    public ProjectileBehaviour CurrentBullet { get; private set; }

    private void Start() {
        ComponentsSL.AddService(GetType(), this);
    }

    public void Shoot()
    {
        Animator.SetTrigger("Shoot");
        FireFX.Play();

        CurrentBullet = Instantiate(Data.Projectile, Barrel.position, Quaternion.identity) as ProjectileBehaviour;
        CurrentBullet.SetDamage(Data.Damage);

        var soundManager = ManagersSL.GetService(typeof(SoundManager)) as SoundManager;
        soundManager?.PlaySoundWithRandomPitching("SlowMotionShoot");
    }
}
