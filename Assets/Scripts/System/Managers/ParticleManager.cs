using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : ManagerService
{
    Dictionary<string, ParticleSystem> Particles = new Dictionary<string, ParticleSystem>();

    public ParticleManager(){
        LoadParticles();
    }

    public void PlayParticles(string _name, Vector3 _position){
        var particles = Particles[_name];

        if(!particles) return;
        
        PlayParticles(particles, _position);
    }

    public void PlayParticles(ParticleSystem _particleSystem, Vector3 _position){
        if(!_particleSystem) throw new System.Exception("Particle system equals null");

        ParticleSystem particleSystem = GameObject.Instantiate(_particleSystem, _position, Quaternion.identity);

        particleSystem.gameObject.name = "Particles(" + _particleSystem.name + ")";
        particleSystem.Play();

        GameObject.Destroy(particleSystem.gameObject, particleSystem.main.startLifetime.constant);
    }

    void LoadParticles(){
        var particlesArray = Resources.LoadAll<ParticleSystem>("Particles");
        
        foreach (var particles in particlesArray)
        {
            Particles.Add(particles.name, particles);
        }
    }
}
