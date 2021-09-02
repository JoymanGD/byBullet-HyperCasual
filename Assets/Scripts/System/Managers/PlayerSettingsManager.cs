using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerSettingsManager : ManagerService
{
    Dictionary<string, ProjectileBehaviour> Projectiles = new Dictionary<string, ProjectileBehaviour>();
    Dictionary<string, Player> Players = new Dictionary<string, Player>();
    Dictionary<string, Gun> Guns = new Dictionary<string, Gun>();

    bool AudioSetUp = false;

    public PlayerSettingsManager(){
        SetupPlayer();
        SetupSettings();
        
    }

    void SetupSettings()
    {
        LoadAudioSettings();
        var settingsButtonObject = GameObject.Find("SettingsBtn");
        var settingsButton = settingsButtonObject.GetComponent<Button>();
        settingsButton.onClick.AddListener(()=> {
            if(!AudioSetUp){
                AudioSetUp = true;
                SetupAudio();
            }
        });
    }

    private void SetupPlayer()
    {
        if(!PlayerPrefs.HasKey("CurrentPlayer")){
            PlayerPrefs.SetString("CurrentPlayer", "Default");
        }

        if(!PlayerPrefs.HasKey("CurrentGun")){
            PlayerPrefs.SetString("CurrentGun", "Default");
        }

        if(!PlayerPrefs.HasKey("CurrentProjectile")){
            PlayerPrefs.SetString("CurrentProjectile", "Default");
        }

        LoadDictionaries();
    }

    public ProjectileBehaviour GetProjectile(string _name){
        var projectile = Projectiles[_name];

        if(!projectile) throw new Exception("There is no such Projectile prefab");

        return projectile;
    }

    public Player GetPlayer(string _name){
        var player = Players[_name];

        if(!player) throw new Exception("There is no such Player prefab");

        return player;
    }

    public Gun GetGun(string _name){
        var gun = Guns[_name];

        if(!gun) throw new Exception("There is no such Gun prefab");

        return gun;
    }

    private void LoadDictionaries()
    {
        LoadPlayers();
        LoadProjectiles();
        LoadGuns();
    }

    void LoadPlayers(){
        var players = Resources.LoadAll<Player>("Prefabs/Players");
        
        foreach (var player in players)
        {
            Players.Add(player.name, player);
        }
    }

    void LoadProjectiles(){
        var projectiles = Resources.LoadAll<ProjectileBehaviour>("Prefabs/Projectiles");
        
        foreach (var projectile in projectiles)
        {
            Projectiles.Add(projectile.name, projectile);
        }
    }

    void LoadGuns(){
        var guns = Resources.LoadAll<Gun>("Prefabs/Guns");
        
        foreach (var gun in guns)
        {
            Guns.Add(gun.name, gun);
        }
    }

    void SetupAudio(){
        var soundSliderObject = GameObjectsSL.GetService("SoundSlider");
        var musicSliderObject = GameObjectsSL.GetService("MusicSlider");
        var soundSlider = soundSliderObject.GetComponent<Slider>();
        var musicSlider = musicSliderObject.GetComponent<Slider>();

        float soundValue = PlayerPrefs.GetFloat("SoundVolume");
        float musicValue = PlayerPrefs.GetFloat("MusicVolume");

        soundSlider.value = soundValue;
        musicSlider.value = musicValue;

        SubscribeAudioGroups(soundSlider, musicSlider);

        var settingsBackButtonObject = GameObjectsSL.GetService("SettingsBackButton");
        var settingsBackButton = settingsBackButtonObject.GetComponent<Button>();
        settingsBackButton.onClick.AddListener(()=> SaveAudioSettings(soundSlider, musicSlider));
    }

    void SubscribeAudioGroups(Slider _soundSlider, Slider _musicSlider)
    {
        _soundSlider.onValueChanged.AddListener((value)=> AudioManager.SetVolume("Sound", Denormalize(value, AudioManager.MinVolume, AudioManager.MaxVolume)));
        _musicSlider.onValueChanged.AddListener((value)=> AudioManager.SetVolume("Music", Denormalize(value, AudioManager.MinVolume, AudioManager.MaxVolume)));
    }

    public void LoadAudioSettings(){
        if(!PlayerPrefs.HasKey("SoundVolume")){
            PlayerPrefs.SetFloat("SoundVolume", .7f);
        }
        
        if(!PlayerPrefs.HasKey("MusicVolume")){
            PlayerPrefs.SetFloat("MusicVolume", .7f);
        }

        float soundValue = PlayerPrefs.GetFloat("SoundVolume");
        float musicValue = PlayerPrefs.GetFloat("MusicVolume");
        
        AudioManager.SetVolume("Sound", Denormalize(soundValue, AudioManager.MinVolume, AudioManager.MaxVolume));
        AudioManager.SetVolume("Music", Denormalize(musicValue, AudioManager.MinVolume, AudioManager.MaxVolume));
    }

    public void SaveAudioSettings(Slider _soundSlider, Slider _musicSlider){
        PlayerPrefs.SetFloat("SoundVolume", _soundSlider.value);
    
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
    }

    float Denormalize(float _value, float _min, float _max){
        return (_value * (_max - _min)) + _min;
    }
}
