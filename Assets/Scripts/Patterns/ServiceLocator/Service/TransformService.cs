using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TransformService : MonoService
{
    [SerializeField] string Name;
    protected override void Init()
    {
        TransformsSL.AddService(Name, transform);
    }
}
