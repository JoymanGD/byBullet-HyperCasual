using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : ManagerService
{
    Dictionary<string, AudioClip> Sounds = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> Musics = new Dictionary<string, AudioClip>();

    static float SoundVolume, MusicVolume;
    static AudioMixer Mixer;
    public static readonly float MaxVolume = 10f, MinVolume = -80f;
    
    static AudioManager(){
        Mixer = Resources.Load<AudioMixer>("Audio/Main");
    }

    public AudioManager(){
        LoadSounds();
        LoadMusics();
        LoadAudioSettings();

        PlayMusic("Casual", true);
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

    public void LoadAudioSettings(){
        if(!PlayerPrefs.HasKey("SoundVolume")){
            PlayerPrefs.SetFloat("SoundVolume", .7f);
        }
        
        if(!PlayerPrefs.HasKey("MusicVolume")){
            PlayerPrefs.SetFloat("MusicVolume", .7f);
        }

        SoundVolume = PlayerPrefs.GetFloat("SoundVolume");
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        
        AudioManager.SetVolume("Sound", SoundVolume);
        AudioManager.SetVolume("Music", MusicVolume);
        // AudioManager.SetVolume("Sound", Denormalize(soundValue, AudioManager.MinVolume, AudioManager.MaxVolume));
        // AudioManager.SetVolume("Music", Denormalize(musicValue, AudioManager.MinVolume, AudioManager.MaxVolume));
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

    public static void SaveAudioSettings(){
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
    
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
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

    public static void SetVolume(string _channel, float _value){
        Mixer.SetFloat(_channel, Mathf.Log10(_value) * 20);

        if(_channel == "Sound")
            SoundVolume = _value;
        else if(_channel == "Music")
            MusicVolume = _value;
    }
}