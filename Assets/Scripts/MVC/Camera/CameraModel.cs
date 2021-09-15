using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waldem.Unity.Events;

public class CameraModel : MonoBehaviour
{
    [SerializeField] CustomEvent<Transform, float, float> OnCameraShake;
    [SerializeField] CustomEvent<Transform, Transform, float, Vector3> OnTargetMoved;
    [SerializeField] Transform CameraTransform;
    [SerializeField] Transform Parent;
    [SerializeField] float CameraFollowSpeed;
    Transform Target;
    Vector3 Offset;

    public void SetTarget(Transform _target){
        Target = _target;
        
        if(Target) SetOffset();
    }

    public void TargetWasMoved(){
        OnTargetMoved?.Invoke(Parent, Target, CameraFollowSpeed, Offset);
    }

    void SetOffset(){
        Offset = Parent.position - Target.position;
    }

    public void ShakeCamera(float _duration, float _strength){
        OnCameraShake?.Invoke(CameraTransform, _duration, _strength);
    }
}
