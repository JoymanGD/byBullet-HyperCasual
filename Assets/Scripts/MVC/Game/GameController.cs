using UnityEngine;
using Waldem.Unity.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] ClassicEvent OnSettingUp, OnApplicationStart, OnGameStart, OnPause, OnResume, OnGameOver, OnApplicationEnd;

    private void Start() {
        OnSettingUp?.Invoke();
        OnApplicationStart?.Invoke();
    }

    public void StartGame(){
        OnGameStart?.Invoke();
    }

    public void Pause(){
        OnPause?.Invoke();
    }

    public void GameOver(){
        OnGameOver?.Invoke();
    }

    private void OnDisable() {
        OnApplicationEnd?.Invoke();
    }
}