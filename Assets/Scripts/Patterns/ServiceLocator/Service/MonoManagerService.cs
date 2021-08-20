using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MonoManagerService : MonoService, IManager
{
    protected override void Init()
    {
        ManagersSL.AddService(GetType(), this);
    }
}
