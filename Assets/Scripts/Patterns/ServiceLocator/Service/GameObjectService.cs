using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameObjectService : MonoService
{
    [SerializeField] string Name;
    protected override void Init()
    {
        GameObjectsSL.AddService(Name, gameObject);
    }
}
