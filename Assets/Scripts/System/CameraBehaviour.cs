using UnityEngine;
using DG.Tweening;

public class CameraBehaviour : MonoComponentService, ICamera
{
    [SerializeField] private float CameraSpeed;
    Transform Target;
    Vector3 Offset;
    Transform Transform;
    Transform ParentTransform; // need to shake camera without locking transform position
    Camera Camera;
    bool FullFollowing = false;

    protected override void Awake() {
        base.Awake();
        
        Transform = transform;
        Camera = GetComponent<Camera>();
        ParentTransform = Transform.parent;
    }

    private void LateUpdate() {
        if(Target){
            FollowTarget();
        }
    }

    public void SetTarget(Transform _target){
        Target = _target;
        
        if(Target) SetOffset();
    }

    public void SetOffset(){
        Offset = ParentTransform.position - Target.position;
    }

    public void FollowTarget(){
        Vector3 finalPos;

        if(FullFollowing)
            finalPos = Target.position + Offset;
        else {
            var offsetDepth = Target.position.z + Offset.z;
            var z = offsetDepth > ParentTransform.position.z ? offsetDepth : ParentTransform.position.z;
            finalPos = new Vector3(ParentTransform.position.x, ParentTransform.position.y, z);
        }

        ParentTransform.position = Vector3.Lerp(ParentTransform.position, finalPos, CameraSpeed * Time.smoothDeltaTime);
    }

    public void Shake(float _duration, float _strength=3, int _vibrato=10, float _randomness=90, bool _fadeout=true){
        Camera.DOShakePosition(_duration, _strength, _vibrato, _randomness, _fadeout).SetUpdate(true).OnComplete(ResetCameraX);
    }

    void ResetCameraX(){
        Transform.position = new Vector3(0, Transform.position.y, Transform.position.z);
    }
}