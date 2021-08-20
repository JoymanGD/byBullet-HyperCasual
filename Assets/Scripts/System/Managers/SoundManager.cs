using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : ManagerService
{
    Dictionary<string, AudioClip> Audio = new Dictionary<string, AudioClip>();
    
    public SoundManager(){
        LoadSounds();
    }

    public void PlaySound(string _name){
        var sound = GetSound(_name);

        var audioSource = CreateAudioSource(_name);

        audioSource.PlayOneShot(sound);

        GameObject.Destroy(audioSource.gameObject, sound.length);
    }

    public void PlaySoundWithRandomPitching(string _name){
        var sound = GetSound(_name);

        var audioSource = CreateAudioSource(_name);

        audioSource.pitch = UnityEngine.Random.Range(.6f, .9f);

        audioSource.PlayOneShot(sound);

        GameObject.Destroy(audioSource.gameObject, sound.length);
    }

    public AudioClip GetSound(string _name){
        var sound = Audio[_name];

        if(!sound) new Exception("There is no such sound: " + _name);

        return sound;
    }

    public AudioSource CreateAudioSource(string _name){
        GameObject soundObject = new GameObject("Sound(" + _name + ")");

        return soundObject.AddComponent<AudioSource>();
    }

    void LoadSounds(){
        var sounds = Resources.LoadAll<AudioClip>("Sounds");
        
        foreach (var sound in sounds)
        {
            Audio.Add(sound.name, sound);
        }
    }
}
