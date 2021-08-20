using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoService : MonoBehaviour
{
    protected virtual void Awake() {
        Init();
    }
    
    protected abstract void Init();
}