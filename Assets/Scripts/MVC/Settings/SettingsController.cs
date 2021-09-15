using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waldem.Unity.Events;

public class SettingsController : MonoBehaviour
{
    [SerializeField] BoolEvent OnToggle;

    public void Toggle(bool _value){
        OnToggle?.Invoke(_value);
    }
}
