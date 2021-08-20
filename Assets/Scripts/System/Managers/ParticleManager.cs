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

        ParticleSystem particleSystem = GameObject.Instantiate(particles, _position, Quaternion.identity);
        particleSystem.gameObject.name = "Particles(" + _name + ")";
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
