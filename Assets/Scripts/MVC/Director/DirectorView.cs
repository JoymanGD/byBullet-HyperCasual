using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Waldem.Unity.Events;

public class DirectorView : MonoBehaviour
{
    [SerializeField] ClassicEvent OnStartAnimationPlay, OnStartAnimationStopped;
    [SerializeField] PlayableDirector Director;

    public void PlayStartAnimation(){
        Director.stopped += (dir)=> OnStartAnimationStopped?.Invoke();
        Director.Play();
    }

    public void StartAnimationBegin(){
        OnStartAnimationPlay?.Invoke();
    }
}
