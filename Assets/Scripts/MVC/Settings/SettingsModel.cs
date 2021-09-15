using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waldem.Unity.Events;

public class SettingsModel : MonoBehaviour
{
    [SerializeField] BoolEvent OnToggle;
    [SerializeField] bool Enabled = false;

    public void Toggle(bool _value){
        Enabled = _value;
        Debug.Log("Status: " + _value);

        OnToggle?.Invoke(Enabled);
    }
}
