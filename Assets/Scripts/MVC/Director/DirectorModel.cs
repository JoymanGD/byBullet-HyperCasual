using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waldem.Unity.Events;

public class DirectorModel : MonoBehaviour
{
    [SerializeField] ClassicEvent OnStartAnimationPlay;

    public void PlayStartAnimation(){
        OnStartAnimationPlay?.Invoke();
    }
}
