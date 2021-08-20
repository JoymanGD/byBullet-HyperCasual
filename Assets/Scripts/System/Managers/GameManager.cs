using UnityEngine;

public class GameManager : ManagerService
{
    [SerializeField] GameObject TapText;
    public static bool GameStarted;
    private int PlayerHealth;
    private bool StartTapped = false;

    public GameManager(){
        GameStarted = false;
    }

    public void StartTap(bool _tutorial){
        if(!StartTapped){
            StartTapped = true;
            
            var tapText = GameObjectsSL.GetService("TapText");
            tapText?.SetActive(false);

            var pistol = ComponentsSL.GetService(typeof(Gun)) as Gun;
            pistol.Shoot();

            var startGameDirector = PlayableDirectorsSL.GetService("StartGame");

            var timeManager = ManagersSL.GetService(typeof(TimeManager)) as TimeManager;
            timeManager.DoSlowMotion();

            startGameDirector.Play();
            startGameDirector.stopped += (director)=> {
                if(_tutorial)
                    StartTutorial(timeManager);
                else
                    StartGame(timeManager);
            };
        }
    }

    private void StartGame(TimeManager _timeManager)
    {
        var bulletTransform = TransformsSL.GetService("MainBullet");
        
        var cameraBehaviour = ComponentsSL.GetService(typeof(CameraBehaviour)) as CameraBehaviour;
        cameraBehaviour.SetTarget(bulletTransform);

        GameStarted = true;
        Debug.Log("Game started");
    }

    private void StartTutorial(TimeManager _timeManager)
    {

    }

    public void SetHealth(int _value){
        PlayerHealth = _value;
    }

    public void GetDamage(int _value, bool _shake = false, bool _sound = false){
        PlayerHealth -= _value;
        CheckHealth(ref PlayerHealth);
    }

    public void CheckHealth(ref int _health){
        if(_health <= 0){
            _health = 0;
            GameOver();
        }
    }

    public void GameOver(){
        Debug.Log("GameOver");
    }
}
