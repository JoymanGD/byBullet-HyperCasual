using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Service
{
    protected Service(){
        Init();
    }
    
    protected abstract void Init();
}