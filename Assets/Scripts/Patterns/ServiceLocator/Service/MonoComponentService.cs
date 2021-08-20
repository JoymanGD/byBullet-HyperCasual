using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MonoComponentService : MonoService
{
    protected override void Init()
    {
        ComponentsSL.AddService(GetType(), this);
    }
}
