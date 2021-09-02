using System;
using System.Collections;
using UnityEngine;

public class VisibleObject : MonoBehaviour
{
    public Action OnAppear, OnDisappear;
    public bool IsVisible { get; private set; } = false;
    protected Transform Transform;
    Camera MainCamera;

    void OnEnable()
    {
        Transform = transform;
        MainCamera = Camera.main;

        StartCoroutine(CheckForVisibility());
    }

    IEnumerator CheckForVisibility(){
        var viewPosition = MainCamera.WorldToViewportPoint(Transform.position);
        var insideViewport = InsideViewport(viewPosition);
        
        if(insideViewport && !IsVisible){
            OnAppear?.Invoke();
            IsVisible = true;
        }
        else if(!insideViewport && IsVisible){
            OnDisappear?.Invoke();
            IsVisible = false;
        }

        yield return new WaitForSeconds(.2f);

        StartCoroutine(CheckForVisibility());
    }

    bool InsideViewport(Vector3 _position){
        return (_position.x >= 0 && _position.x <= 1 && _position.y >= 0 && _position.y <= 1) && _position.z > 0;
    }
}
