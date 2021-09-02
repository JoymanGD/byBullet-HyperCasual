using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : VisibleObject
{
    [SerializeField] protected EnemyData Data;
    public int CurrentHealth { get; private set; }
    public bool Alive { get; private set; } = true;

    protected virtual void MonoStart(){}
    
    protected void Start() {
        var physicsManager = ManagersSL.GetService(typeof(PhysicsManager)) as PhysicsManager;
        physicsManager.ToggleRagdoll(gameObject, false);

        CurrentHealth = Data.Health;

        OnDisappear += SkipTheEnemy;

        MonoStart();

        StartCoroutine(AttackByRate());
    }

    public abstract void Attack();
    
    public void GetDamage(int _value){
        CurrentHealth -= _value;

        var soundManager = ManagersSL.GetService(typeof(AudioManager)) as AudioManager;
        soundManager.PlaySoundWithRandomPitching("Hit");

        var particleManager = ManagersSL.GetService(typeof(ParticleManager)) as ParticleManager;
        particleManager.PlayParticles("Blood", Transform.position + Vector3.up);

        var camera = ComponentsSL.GetService(typeof(CameraBehaviour)) as CameraBehaviour;
        camera.Shake(.5f, .7f);

        CheckHealth(CurrentHealth);
    }

    public void CheckHealth(int _value){
        if(_value <= 0){
            Die();
        }
    }

    public virtual void Die(){
        Alive = false;

        var physicsManager = ManagersSL.GetService(typeof(PhysicsManager)) as PhysicsManager;
        physicsManager.ToggleRagdoll(gameObject, true);
        physicsManager.ThrowRagdollRandomly(gameObject);
    }

    private void SkipTheEnemy() {
        if(Alive){
            var gameManager = ManagersSL.GetService(typeof(GameManager)) as GameManager;
            gameManager.GetDamage(Data.Damage);
            Destroy(gameObject);
        }
    }

    IEnumerator AttackByRate(){
        if(!Alive) yield break;
        
        yield return new WaitForSeconds(Data.AttackRate);
        Attack();
        StartCoroutine(AttackByRate());
    }
}