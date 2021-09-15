using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Waldem.Unity.Events;

public class CameraController : MonoBehaviour
{
    [SerializeField] CustomEvent<Transform> OnTargetSet;
    [SerializeField] ClassicEvent OnTargetMoved;
    [SerializeField] CustomEvent<float, float> OnCameraShake;

    public void SetTarget(Transform _target){
        OnTargetSet?.Invoke(_target);
    }

    public void FollowTarget(Transform _parentTransform, Transform _target, float _cameraSpeed, Vector3 _offset){
        Vector3 finalPos;
        var offsetDepth = _target.position.z + _offset.z;
        var z = offsetDepth > _parentTransform.position.z ? offsetDepth : _parentTransform.position.z;
        finalPos = new Vector3(_parentTransform.position.x, _parentTransform.position.y, z);

        _parentTransform.position = Vector3.Lerp(_parentTransform.position, finalPos, _cameraSpeed * Time.smoothDeltaTime);
    }

    public void ShakeCamera(float _duration, float _strength){
        OnCameraShake?.Invoke(_duration, _strength);
    }

    public void ResetCameraX(Transform _transform){
        _transform.position = new Vector3(0, _transform.position.y, _transform.position.z);
    }
}
