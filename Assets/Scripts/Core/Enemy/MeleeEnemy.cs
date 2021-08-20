using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MeleeEnemy : Enemy
{
    NavMeshAgent Agent;

    protected override void MonoStart()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.enabled = false;
    }

    public override void Attack()
    {
        if(Agent.enabled){
            if(Agent.remainingDistance < 2){
                var gameManager = ManagersSL.GetService(typeof(GameManager)) as GameManager;
                gameManager.GetDamage(Data.Damage);
            }
        }
    }

    public override void Die()
    {
        base.Die();

        Agent.enabled = false;
    }

    private void OnBecameVisible() {
        Agent.enabled = true;
        Agent.destination = TransformsSL.GetService("Player").position;
        Agent.speed = Data.Speed;
        Debug.Log($"'{name}' is Visible to '{Camera.current.name}'");
    }
}
