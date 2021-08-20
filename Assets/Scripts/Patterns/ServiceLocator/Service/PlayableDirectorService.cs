using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class PlayableDirectorService : MonoService
{
    [SerializeField] string DirectorName;
    protected override void Init()
    {
        var director = GetComponent<PlayableDirector>();
        PlayableDirectorsSL.AddService(DirectorName, director);
    }
}
