using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Waldem.Unity.Events;

public class AudioModel : MonoBehaviour
{
    [SerializeField] CustomEvent<Dictionary<string, AudioClip>, Dictionary<string, AudioClip>, float, float> OnDataRequest;
    [SerializeField] CustomEvent<float, float> OnSettingsSave;
    [SerializeField] CustomEvent<string, float> OnVolumeSet;
    [SerializeField] CustomEvent<AudioClip, bool> OnMusicPlay;
    [SerializeField] CustomEvent<AudioClip> OnSoundPlay, OnRandomPitchingSoundPlay;
    [SerializeField] float SoundVolume, MusicVolume;
    Dictionary<string, AudioClip> Sounds = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> Musics = new Dictionary<string, AudioClip>();
    
    public void SendData(){
        OnDataRequest?.Invoke(Sounds, Musics, SoundVolume, MusicVolume);
    }

    public void LoadSettings(){
        if(!PlayerPrefs.HasKey("SoundVolume")){
            PlayerPrefs.SetFloat("SoundVolume", .7f);
        }
        
        if(!PlayerPrefs.HasKey("MusicVolume")){
            PlayerPrefs.SetFloat("MusicVolume", .7f);
        }

        SetVolume("Sound", PlayerPrefs.GetFloat("SoundVolume"));
        SetVolume("Music", PlayerPrefs.GetFloat("MusicVolume"));
    }

    public void SetVolume(string _channel, float _value){
        if(_channel == "Sound"){
            SoundVolume = _value;

            OnVolumeSet?.Invoke("Sound", SoundVolume);
        }
        else if(_channel == "Music")
            MusicVolume = _value;

            OnVolumeSet?.Invoke("Music", MusicVolume);
    }

    public void PlaySound(string _name){
        var clip = GetSound(_name);
        OnSoundPlay?.Invoke(clip);
    }

    public void PlaySoundWithRandomPitching(string _name){
        var clip = GetSound(_name);
        OnRandomPitchingSoundPlay?.Invoke(clip);
    }

    public void PlayMusic(string _name, bool _loop){
        var clip = GetMusic(_name);
        OnMusicPlay?.Invoke(clip, _loop);
    }

    AudioClip GetSound(string _name){
        var sound = Sounds[_name];

        if(!sound) new Exception("There is no such sound: " + _name);

        return sound;
    }

    AudioClip GetMusic(string _name){
        var music = Musics[_name];

        if(!music) new Exception("There is no such music: " + _name);

        return music;
    }

    public void SaveAudioSettings(){
        OnSettingsSave?.Invoke(SoundVolume, MusicVolume);
    }
}