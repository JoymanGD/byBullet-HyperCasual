using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoComponentService
{
    [SerializeField] PlayerData Data;

    protected override void Awake()
    {
        base.Awake();
        
        var gameManager = ManagersSL.GetService(typeof(GameManager)) as GameManager;
        gameManager.SetHealth(Data.Health);
    }
}