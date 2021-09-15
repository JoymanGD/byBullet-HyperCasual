using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waldem.Unity.Events;

public class DirectorController : MonoBehaviour
{
    [SerializeField] ClassicEvent OnStartAnimationPlay;

    public void PlayStartAnimation(){
        OnStartAnimationPlay?.Invoke();
    }
}
