using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : ManagerService
{
    Dictionary<string, AudioClip> Sounds = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> Musics = new Dictionary<string, AudioClip>();

    static AudioMixer Mixer;
    public static readonly float MaxVolume = 10f, MinVolume = -80f;
    
    static AudioManager(){
        Mixer = Resources.Load<AudioMixer>("Audio/Main");
    }

    public AudioManager(){
        LoadSounds();
        LoadMusics();

        PlayMusic("Casual", true);
    }


    public void PlaySound(AudioClip _clip){
        var audioSource = CreateAudioSource("Sound("+_clip.name+")");
        PlaySound(_clip, audioSource);
    }

    public void PlaySound(AudioClip _clip, AudioSource _source){
        _source.outputAudioMixerGroup = Mixer.FindMatchingGroups("Sound")[0];
        _source.PlayOneShot(_clip);

        GameObject.Destroy(_source.gameObject, _clip.length);
    }

    public void PlaySound(string _name){
        var sound = GetSound(_name);

        PlaySound(sound);
    }

    public void PlaySoundWithRandomPitching(string _name){
        var sound = GetSound(_name);

        var audioSource = CreateAudioSource("Sound("+_name+")");

        audioSource.pitch = UnityEngine.Random.Range(.6f, .9f);

        PlaySound(sound, audioSource);
    }

    public void PlaySoundWithRandomPitching(AudioClip _clip){
        var audioSource = CreateAudioSource("Sound("+_clip.name+")");

        audioSource.pitch = UnityEngine.Random.Range(.6f, .9f);

        PlaySound(_clip, audioSource);
    }

    public void PlayMusic(string _name, bool _loop){
        var clip = GetMusic(_name);
        var audioSource = CreateAudioSource("Music("+clip.name+")");
        PlayMusic(clip, audioSource, _loop);
    }

    public void PlayMusic(AudioClip _clip, bool _loop){
        var audioSource = CreateAudioSource("Music("+_clip.name+")");
        PlayMusic(_clip, audioSource, _loop);
    }

    public void PlayMusic(AudioClip _clip, AudioSource _source, bool _loop){
        _source.outputAudioMixerGroup = Mixer.FindMatchingGroups("Music")[0];
        _source.clip = _clip;
        _source.loop = _loop;
        _source.Play();

        if(!_loop)
            GameObject.Destroy(_source.gameObject, _clip.length);
    }

    public AudioClip GetSound(string _name){
        var sound = Sounds[_name];

        if(!sound) new Exception("There is no such sound: " + _name);

        return sound;
    }

    public AudioClip GetMusic(string _name){
        var music = Musics[_name];

        if(!music) new Exception("There is no such music: " + _name);

        return music;
    }

    public AudioSource CreateAudioSource(string _name){
        GameObject soundObject = new GameObject(_name);

        return soundObject.AddComponent<AudioSource>();
    }

    void LoadSounds(){
        var sounds = Resources.LoadAll<AudioClip>("Audio/Sounds");
        
        foreach (var sound in sounds)
        {
            Sounds.Add(sound.name, sound);
        }
    }

    void LoadMusics(){
        var musics = Resources.LoadAll<AudioClip>("Audio/Musics");
        
        foreach (var music in musics)
        {
            Musics.Add(music.name, music);
        }
    }

    public static void SetVolume(string _channel, float _value){
        float sound, music;

        Mixer.GetFloat("Sound", out sound);
        Mixer.GetFloat("Music", out music);

        Mixer.SetFloat(_channel, _value);

        Mixer.GetFloat("Sound", out sound);
        Mixer.GetFloat("Music", out music);
    }
}
