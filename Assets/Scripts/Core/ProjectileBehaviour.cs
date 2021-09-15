using UnityEngine;
using DG.Tweening;
using System;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] ProjectileData Data;
    public Vector3 Direction { get; private set; }
    public float CurrentSpeedModifier { get; private set; }
    public int CurrentRicochets { get; private set; }
    public int CurrentDamage { get; private set; }
    Transform Transform;
    Rigidbody Rigidbody;
    Vector3 MovementPosition;
    Camera MainCamera;
    bool IsReflecting = false;
    const float MovementSensitivity = .4f;
    const float RotationSensitivity = .2f;
    const float ReflectLength = 2f;
    const float ReflectTime = 1f;
    const float MaxSpeed = 20f;
    const float StartSpeedModifier = 2f;

    private void Start() {
        Transform = transform;
        Rigidbody = GetComponent<Rigidbody>();
        MainCamera = Camera.main;

        SetSpeedModifier(StartSpeedModifier);
        SetRicochets(Data.StartRicochets);

        Direction = Transform.forward;
        MovementPosition = Transform.position;
    }

    public void SetRicochets(int _value)
    {
        CurrentRicochets = _value;
    }

    public void SetDamage(int _value)
    {
        CurrentDamage = _value;
    }

    public void WasteRicochet(Collider _other){
        CurrentRicochets--;
        
        var soundManager = ManagersSL.GetService(typeof(AudioManager)) as AudioManager;
        soundManager.PlaySoundWithRandomPitching(Data.RicochetSFX);

        var particleManager = ManagersSL.GetService(typeof(ParticleManager)) as ParticleManager;
        particleManager.PlayParticles("Sparks", _other.ClosestPoint(Transform.position));

        bool shaked = false;

        if(CurrentRicochets <= 0){
            CurrentRicochets = 0;
            var gameManager = ManagersSL.GetService(typeof(GameManager)) as GameManager;
            gameManager.GetDamage(1, true);
            shaked = true;
        }

        if(!shaked){
            var camera = ComponentsSL.GetService(typeof(CameraBehaviour)) as CameraBehaviour;
            camera.Shake(.3f, .4f);
        }
    }

    public void AddRicochet(){
        CurrentRicochets++;
    }

    public void SetSpeedModifier(float _value){
        CurrentSpeedModifier = _value;
    }

    public void SetDirection(Vector3 _direction){
        var magn = _direction.magnitude;

        if(magn > 2f){
            var cameraPlane = GetCameraPlane();
            Direction = cameraPlane * _direction;
        }
    }

    Quaternion GetCameraPlane(){        
        var cameraTransform = TransformsSL.GetService("MainCamera");
        var cameraPlane = new Quaternion(0, cameraTransform.rotation.y, 0, 0);

        return cameraPlane;
    }

    public void AddMovement(Vector3 _value){
        MovementPosition += Vector3.ClampMagnitude(_value, MaxSpeed) * Time.deltaTime * CurrentSpeedModifier;
    }

    private void Update() {
        if(!IsReflecting){
            Rotate();
        }

        if(GameManager.GameStarted)
            CheckForCollidingBackBound();
    }

    private void FixedUpdate() {
        if(!IsReflecting){
            Move();
        }
    }

    private void CheckForCollidingBackBound()
    {
        var viewPosition = MainCamera.WorldToViewportPoint(Transform.position);

        if(!IsReflecting && viewPosition.y <= 0){
            ReflectBackBound();
        }
    }

    private void Move(){
        Transform.position = Vector3.Lerp(Transform.position, MovementPosition, MovementSensitivity);
    }

    void Rotate(){
        Transform.rotation = Quaternion.Lerp(Transform.rotation, Quaternion.LookRotation(Direction), RotationSensitivity);
    }

    private void OnTriggerEnter(Collider _other) {
        CheckForCollision(_other);
    }

    void CheckForCollision(Collider _other){
        if(_other.gameObject.layer == 6){ //enemy
            var enemy = _other.GetComponent<Enemy>();

            if(enemy){
                enemy.GetDamage(CurrentDamage);
            }
        }
        else if(_other.gameObject.layer == 7){ //ricochet
            WasteRicochet(_other);
        }
        else if(_other.gameObject.layer == 9){ //finish
            var finishComponent = _other.GetComponent<Finish>();
            finishComponent.FinishGame();
        }
        else if(_other.gameObject.layer == 10){ //obstacle
            var hitPoint = _other.ClosestPoint(Transform.position);
            
            var gameManager = ManagersSL.GetService(typeof(GameManager)) as GameManager;
            gameManager.GetDamage(1);

            var particleManager = ManagersSL.GetService(typeof(ParticleManager)) as ParticleManager;
            particleManager.PlayParticles("ShotSmoke", hitPoint);

            var soundManager = ManagersSL.GetService(typeof(AudioManager)) as AudioManager;
            soundManager.PlaySoundWithRandomPitching(Data.ObstacleSFX);

            ReflectObstacle();
        }
    }

    private void ReflectObstacle()
    {
        RaycastHit forwardHit;
        RaycastHit backHit = new RaycastHit(); //need to initialize because of 'using local var without declaring'

        var projectileLayerMask = 1 << 10;

        var rayDirection = Transform.forward;

        if(Physics.Raycast(Transform.position, rayDirection, out forwardHit, 10, projectileLayerMask) || Physics.Raycast(Transform.position, -rayDirection, out backHit, 100, projectileLayerMask)){
            var hit = forwardHit;
            float finalReflectLength = ReflectLength;

            if(!hit.collider){
                hit = backHit;
                finalReflectLength *= 2;
            }
            
            Reflect(hit.normal, finalReflectLength);
        }
        else{
            throw new System.Exception("Reflection caused error");
        }
    }

    void ReflectBackBound(){
        Reflect(GetCameraPlane() * Vector3.forward, ReflectLength);

        var particleManager = ManagersSL.GetService(typeof(ParticleManager)) as ParticleManager;
        particleManager.PlayParticles("ShotSmoke", Transform.position);

        var soundManager = ManagersSL.GetService(typeof(AudioManager)) as AudioManager;
        soundManager.PlaySoundWithRandomPitching("HitThrough");
    }

    void Reflect(Vector3 _normal, float _reflectLength, Action _onComplete = null){
        IsReflecting = true;

        var timeManager = ManagersSL.GetService(typeof(TimeManager)) as TimeManager;
        timeManager.ResetTimeScale();

        var playerController = ComponentsSL.GetService(typeof(PlayerControllerOld)) as PlayerControllerOld;
        playerController.ControllingTime = false;

        var reflectedDirection = Vector3.Reflect(Transform.forward, _normal);
        var reflectPosition = Transform.position + reflectedDirection * _reflectLength;
        Transform.rotation = Quaternion.LookRotation(reflectedDirection, Vector3.up);

        Transform.DOMove(reflectPosition, ReflectTime).OnComplete(()=>{
            playerController.ControllingTime = true;
            MovementPosition = Transform.position;
            Direction = reflectedDirection;
            IsReflecting = false;

            _onComplete?.Invoke();
        });
    }
}