using UnityEngine;

public class Gun : MonoComponentService, IShooting
{
    [SerializeField] GunData Data;
    [SerializeField] Transform Barrel;
    public ProjectileBehaviour CurrentBullet { get; private set; }

    public void Shoot()
    {
        var player = ComponentsSL.GetService(typeof(Player)) as Player;
        player.ReactOnShot();

        var particleManager = ManagersSL.GetService(typeof(ParticleManager)) as ParticleManager;
        particleManager.PlayParticles(Data.ShotGFX, Barrel.position);

        var projectileName = PlayerPrefs.GetString("CurrentProjectile");
        var playerSettingManager = ManagersSL.GetService(typeof(PlayerSettingsManager)) as PlayerSettingsManager;
        var projectile = playerSettingManager.GetProjectile(projectileName);

        CurrentBullet = Instantiate(projectile, Barrel.position, Quaternion.identity) as ProjectileBehaviour;
        CurrentBullet.SetDamage(Data.Damage);
        
        ComponentsSL.AddService(CurrentBullet.GetType(), CurrentBullet);

        var soundManager = ManagersSL.GetService(typeof(AudioManager)) as AudioManager;
        soundManager?.PlaySoundWithRandomPitching(Data.ShotSFX);
    }
}
