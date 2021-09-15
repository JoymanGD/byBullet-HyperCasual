using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waldem.Unity.Events;

public class PlayerView : MonoBehaviour
{
    [SerializeField] Animator PlayerAnimator;

    public void Shoot(){
        PlayerAnimator.SetTrigger("Shoot");
    }
}
