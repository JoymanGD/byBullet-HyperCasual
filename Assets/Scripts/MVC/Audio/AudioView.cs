using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioView : MonoBehaviour
{
    [SerializeField] AudioMixer Mixer;

    public void SetVolume(string _channel, float _value){
        Mixer.SetFloat(_channel, Mathf.Log10(_value) * 20);
    }

    public AudioSource CreateAudioSource(string _name){
        GameObject soundObject = new GameObject(_name);

        return soundObject.AddComponent<AudioSource>();
    }
    
    public void PlaySound(AudioClip _clip){
        var audioSource = CreateAudioSource("Sound("+_clip.name+")");
        PlaySoundFromAudioSource(_clip, audioSource);
    }

    void PlaySoundFromAudioSource(AudioClip _clip, AudioSource _source){
        _source.outputAudioMixerGroup = Mixer.FindMatchingGroups("Sound")[0];
        _source.PlayOneShot(_clip);

        GameObject.Destroy(_source.gameObject, _clip.length);
    }

    public void PlaySoundWithRandomPitching(AudioClip _clip){
        var audioSource = CreateAudioSource("Sound("+_clip.name+")");

        audioSource.pitch = UnityEngine.Random.Range(.6f, .9f);

        PlaySoundFromAudioSource(_clip, audioSource);
    }

    public void PlayMusic(AudioClip _clip, bool _loop){
        var audioSource = CreateAudioSource("Music("+_clip.name+")");
        PlayMusicFromAudioSource(_clip, audioSource, _loop);
    }

    void PlayMusicFromAudioSource(AudioClip _clip, AudioSource _source, bool _loop){
        _source.outputAudioMixerGroup = Mixer.FindMatchingGroups("Music")[0];
        _source.clip = _clip;
        _source.loop = _loop;
        _source.Play();

        if(!_loop)
            GameObject.Destroy(_source.gameObject, _clip.length);
    }
    
    public void SaveAudioSettings(float _soundVolume, float _musicVolume){
        PlayerPrefs.SetFloat("SoundVolume", _soundVolume);
    
        PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
    }
}