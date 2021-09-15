using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waldem.Unity.Events;

public class AudioController : MonoBehaviour
{
    [SerializeField] ClassicEvent OnSetup, OnSettingsLoad, OnSettingsSave;
    [SerializeField] CustomEvent<string, float> OnVolumeSet;
    [SerializeField] CustomEvent<string> OnSoundPlay, OnRandomPitchingSoundPlay;
    [SerializeField] CustomEvent<string, bool> OnMusicPlay;

    public void Setup(){
        OnSetup?.Invoke();
    }

    public void LoadAudio(Dictionary<string, AudioClip> _sounds, Dictionary<string, AudioClip> _musics, float _soundVolume, float _musicVolume){
        LoadSounds(_sounds);
        LoadMusics(_musics);
        LoadSettings(_soundVolume, _musicVolume);
    }

    void LoadSounds(Dictionary<string, AudioClip> _sounds){
        var sounds = Resources.LoadAll<AudioClip>("Audio/Sounds");
        
        foreach (var sound in sounds)
        {
            _sounds.Add(sound.name, sound);
        }
    }

    void LoadMusics(Dictionary<string, AudioClip> _musics){
        var musics = Resources.LoadAll<AudioClip>("Audio/Musics");
        
        foreach (var music in musics)
        {
            _musics.Add(music.name, music);
        }
    }

    public void LoadSettings(float _soundVolume, float _musicVolume){
        OnSettingsLoad?.Invoke();
    }

    public void SaveAudioSettings(){
        OnSettingsSave?.Invoke();
    }

    public void SetSoundVolume(float value){
        OnVolumeSet?.Invoke("Sound", value);
    }

    public void SetMusicVolume(float value){
        OnVolumeSet?.Invoke("Music", value);
    }

    public void PlaySound(string _name){
        OnSoundPlay?.Invoke(_name);
    }

    public void PlaySoundWithRandomPitching(string _name){
        OnRandomPitchingSoundPlay?.Invoke(_name);
    }

    public void PlayMusicOnce(string _name){
        OnMusicPlay?.Invoke(_name, false);
    }

    public void PlayMusicLooped(string _name){
        OnMusicPlay?.Invoke(_name, true);
    }
}