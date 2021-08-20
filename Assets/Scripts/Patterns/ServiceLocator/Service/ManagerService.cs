using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ManagerService : Service, IManager
{

    protected override void Init()
    {
        ManagersSL.AddService(GetType(), this);
    }
}
