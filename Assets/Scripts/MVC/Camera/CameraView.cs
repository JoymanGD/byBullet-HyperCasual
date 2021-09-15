using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Waldem.Unity.Events;

public class CameraView : MonoBehaviour
{
    [SerializeField] CustomEvent<Transform> OnCameraShakeComplete;
    [SerializeField] Camera Camera;

    public void ShakeCamera(Transform _transform, float _duration, float _strength){
        
        Camera.DOShakePosition(_duration, _strength).SetUpdate(true).OnComplete(()=> OnCameraShakeComplete?.Invoke(_transform));
    }
}
