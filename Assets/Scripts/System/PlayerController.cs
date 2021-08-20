using UnityEngine;

public class PlayerController : MonoComponentService
{
    [SerializeField] Gun Gun;
    public PlayerControls PlayerControls { get; private set; }
    bool IsMoving = false, WasMoving = false;
    Vector3 MovingVector;

    protected override void Awake(){
        base.Awake();
        
        PlayerControls = new PlayerControls();
        
        //Game start
        PlayerControls.Main.Tap.started += cntxt => {
            if(!GameManager.GameStarted){
                var gameManager = ManagersSL.GetService(typeof(GameManager)) as GameManager;
                gameManager.StartTap(false);
            }
        };
        
        //Movement2
        PlayerControls.Main.Move.performed += cntxt => {
            if(GameManager.GameStarted){
                if(Gun.CurrentBullet){
                    var dir2 = cntxt.ReadValue<Vector2>();
                    MovingVector = new Vector3(dir2.x, 0, dir2.y);
                    Gun.CurrentBullet.AddMovement(MovingVector);
                    Gun.CurrentBullet.SetDirection(MovingVector);
                }
            }
        };

        PlayerControls.Main.Move.canceled += cntxt => {
            if(GameManager.GameStarted){
                if(Gun.CurrentBullet)
                    MovingVector = Vector3.zero;
            }
        };
    }

    private void LateUpdate() {
        ControlTime();
    }

    void ControlTime(){
        IsMoving = MovingVector.magnitude > 0;
                    
        if(!WasMoving && IsMoving){
            var timeManager = ManagersSL.GetService(typeof(TimeManager)) as TimeManager;
            timeManager.ResetTimeScale();
        }

        // Debug.Log("IsMoving: " + IsMoving + "; Magnitude: " + MovingVector.magnitude + "; WasMoving: " + WasMoving);
        
        if(WasMoving && !IsMoving){
            var timeManager = ManagersSL.GetService(typeof(TimeManager)) as TimeManager;
            timeManager.DoSlowMotion();
        }
        
        WasMoving = IsMoving;
    }

    void OnEnable(){
        PlayerControls.Enable();
    }

    void OnDisable(){
        if(PlayerControls != null) PlayerControls.Disable();
    }
}