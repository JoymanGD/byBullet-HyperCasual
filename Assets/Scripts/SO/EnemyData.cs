using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy", fileName = "NewEnemy")]
public class EnemyData : ScriptableObject
{
    public int Health;
    public float AttackRate;
    public int Damage;
    public float Speed;
}
