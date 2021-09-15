using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waldem.Unity.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ClassicEvent OnShoot;

    public void Shoot(){
        OnShoot?.Invoke();
    }
}
