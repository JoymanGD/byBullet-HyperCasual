using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerOld : MonoComponentService
{
    Gun Gun;
    public PlayerControls PlayerControls { get; private set; }
    public bool ControllingTime = true;
    bool IsMoving = false, WasMoving = false;
    Vector3 MovementVector;

    void Start(){
        Gun = ComponentsSL.GetService(typeof(Gun)) as Gun;
        
        var gameManager = ManagersSL.GetService(typeof(GameManager)) as GameManager;
        // var tapPanelGameObject = GameObjectsSL.GetService("TapPanel");
        // var tapPanelButton = tapPanelGameObject.GetComponent<Button>();
        // tapPanelButton.onClick.AddListener(()=>gameManager.StartTap(false));
        
        PlayerControls = new PlayerControls();
        
        //Move
        PlayerControls.Main.Move.performed += cntxt => {
            if(GameManager.GameStarted){
                if(Gun.CurrentBullet){
                    var dir2 = cntxt.ReadValue<Vector2>();
                    MovementVector = new Vector3(dir2.x, 0, dir2.y);
                    Gun.CurrentBullet.AddMovement(MovementVector);
                    Gun.CurrentBullet.SetDirection(MovementVector);
                }
            }
        };

        PlayerControls.Main.Move.canceled += cntxt => {
            if(GameManager.GameStarted){
                if(Gun.CurrentBullet)
                    ResetMovementVector();
            }
        };

        PlayerControls.Enable();
    }

    private void LateUpdate() {
        if(ControllingTime)
            ControlTime();

    }

    void ControlTime(){
        IsMoving = MovementVector.magnitude > 0;
                    
        if(!WasMoving && IsMoving){
            var timeManager = ManagersSL.GetService(typeof(TimeManager)) as TimeManager;
            timeManager.ResetTimeScale();
        }

        if(WasMoving && !IsMoving){
            var timeManager = ManagersSL.GetService(typeof(TimeManager)) as TimeManager;
            timeManager.DoSlowMotion();
        }
        
        WasMoving = IsMoving;
    }

    public void ResetMovementVector(){
        MovementVector = Vector3.zero;
    }

    void OnEnable(){
        if(PlayerControls != null) PlayerControls.Enable();
    }

    void OnDisable(){
        if(PlayerControls != null) PlayerControls.Disable();
    }
}