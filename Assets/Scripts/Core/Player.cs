using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoComponentService
{
    [SerializeField] PlayerData Data;
    Animator Animator;

    private void Start() {
        var gameManager = ManagersSL.GetService(typeof(GameManager)) as GameManager;
        gameManager.SetHealth(Data.Health);
        Animator = GetComponent<Animator>();
    }

    public void ReactOnShot(){
        Animator.SetTrigger("Shoot");
    }

    public PlayerData GetData(){
        return Data;
    }
}