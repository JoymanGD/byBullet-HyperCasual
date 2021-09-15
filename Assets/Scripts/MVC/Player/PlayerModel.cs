using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waldem.Unity.Events;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] ClassicEvent OnShoot;

    public void Shoot(){
        OnShoot?.Invoke();
    }
}
