using UnityEngine;

[DefaultExecutionOrder(2)]
public abstract class MonoService : MonoBehaviour
{
    protected virtual void Awake() {
        Init();
    }
    
    protected abstract void Init();
}