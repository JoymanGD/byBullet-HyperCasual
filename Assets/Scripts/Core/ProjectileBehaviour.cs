using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] ProjectileData Data;
    public Vector3 Direction { get; private set; }
    public float CurrentSpeedModifier { get; private set; }
    public int CurrentRicochets { get; private set; }
    public int CurrentDamage { get; private set; }
    Transform Transform;
    Rigidbody Rigidbody;
    Vector3 MovementVector;
    const float MovementSensitivity = .4f;
    const float RotationSensitivity = .4f;

    private void Start() {
        Transform = GetComponent<Transform>();
        Rigidbody = GetComponent<Rigidbody>();

        SetSpeedModifier(Data.StartSpeedModifier);
        SetRicochets(Data.StartRicochets);

        Direction = Transform.forward;
        MovementVector = Transform.position;
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
        
        var soundManager = ManagersSL.GetService(typeof(SoundManager)) as SoundManager;
        soundManager.PlaySoundWithRandomPitching("MetalHit");

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
            var cameraTransform = TransformsSL.GetService("MainCamera");
            var cameraPlane = new Quaternion(0, cameraTransform.rotation.y, 0, 0);
            Direction = cameraPlane * _direction;
        }
    }

    public void AddMovement(Vector3 _value){
        MovementVector += _value * Time.deltaTime * CurrentSpeedModifier;
    }

    private void Update() {
        Move();
        Rotate();
    }

    private void Move(){
        Transform.position = Vector3.Lerp(Transform.position, MovementVector, MovementSensitivity);
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
        else if(_other.gameObject.layer == 7){ //enemytool
            WasteRicochet(_other);
        }
        else if(_other.gameObject.layer == 9){ //finish
            var finishComponent = _other.GetComponent<Finish>();
            finishComponent.FinishGame();
        }
        else if(_other.gameObject.layer == 10){ //obstacle

            var gameManager = ManagersSL.GetService(typeof(GameManager)) as GameManager;
            gameManager.GetDamage(1);

            var particleManager = ManagersSL.GetService(typeof(ParticleManager)) as ParticleManager;
            particleManager.PlayParticles("ShotSmoke", _other.ClosestPoint(Transform.position));

            var camera = ComponentsSL.GetService(typeof(CameraBehaviour)) as CameraBehaviour;
            camera.Shake(.5f, .4f);

            var soundManager = ManagersSL.GetService(typeof(SoundManager)) as SoundManager;
            soundManager.PlaySoundWithRandomPitching("HitThrough");
        }
    }
}
