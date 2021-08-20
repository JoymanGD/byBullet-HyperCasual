using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : ManagerService
{
    public void ThrowRagdollTowards(GameObject _ragdoll, Vector3 _direction, float _power = 100f){
        var rbs = _ragdoll.GetComponentsInChildren<Rigidbody>();
        var randomIndex = Random.Range(0, rbs.Length);

        var rb = rbs[randomIndex];
        rb.AddForce(_direction * _power, ForceMode.Impulse);
    }

    public void ThrowRagdollRandomly(GameObject _ragdoll, float _power = 100f){
        var randomDirection = new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f));
        ThrowRagdollTowards(_ragdoll, randomDirection, _power);
    }

    public void ToggleRagdoll(GameObject _ragdoll, bool _value){
        ToggleRigidbodies(_ragdoll, !_value);
        ToggleColliders(_ragdoll, _value);
    }

    void ToggleRigidbodies(GameObject _ragdoll, bool _value){
        var rbs = _ragdoll.GetComponentsInChildren<Rigidbody>();

        foreach(var rb in rbs){
            rb.isKinematic = _value;
        }
    }

    void ToggleColliders(GameObject _ragdoll, bool _value){
        var cldrs = _ragdoll.GetComponentsInChildren<Collider>();

        foreach(var cldr in cldrs){
            if(Same(_ragdoll, cldr.gameObject)){
                cldr.enabled = !_value;
                continue;
            }

            cldr.enabled = _value;
        }
    }

    bool Same(GameObject _firstGO, GameObject _secondGO){
        return _firstGO == _secondGO;
    }
}
